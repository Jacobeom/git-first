using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using Common.Logging;
using TwitterAvailability.Dto;
using TwitterAvailability.Repository;
using TwitterAvailability.TwittsProvider;

namespace TwitterAvailability.Service
{
    public class TwitterService
    {
        private ILog log = LogManager.GetLogger(typeof(TwitterService));
        private TwitterIssueRepository twitterIssueRepository;


        public string TwitterProviderClass
        {
            get { return ConfigurationManager.AppSettings["TwitterProviderClass"]; }
        }
        
        public int DaysBeforeToday
        {
            get { return int.Parse(ConfigurationManager.AppSettings["DaysBeforeToday"]); }
        }

        public TwitterService()
        {
            twitterIssueRepository = new TwitterIssueRepository();
        }
       

        public void Run()
        {
            
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(DaysBeforeToday * -1);
                        
            IList<TwitterIssue> listAvailability=  ProcessIssuesFromTweeter(200);
            
            foreach (TwitterIssue twitterIssue in listAvailability.Where(x => x.TwitterDate > fromDate).OrderBy(x => x.TwitterDate))
            {
                ProcessTwitterIssue(twitterIssue);
            }

        }

        private void ProcessTwitterIssue(TwitterIssue twitterIssue)
        {
            log.Info(string.Format("Processing twitter, with TwitterInternalID ={0}",twitterIssue.TwitterId));
            try
            {
                TwitterIssue twitterInDB = null;
                twitterInDB = twitterIssueRepository.FindByTweeterId(twitterIssue.TwitterId);


                if (twitterInDB == null)
                {
                    Product product = FindProduct(twitterIssue.ProductKey);

                    TwitterLifecycle.TwitterLifecycle lifecycle = TwitterLifecycle.TwitterLifecycle.Factory(twitterIssue);
                    TwitterIssue parent = lifecycle.FindParent();
                    bool isLeaf = lifecycle.IsLeaf;

                    if (product != null && product.Id.HasValue)
                    {
                        twitterIssue.ProductId = product.Id.Value;
                        if (parent != null && parent.Id.HasValue)
                        {
                            twitterIssue.PreviousTwittId = parent.Id.Value;
                            twitterIssue.Finished = isLeaf;
                            //In case of #BTPC,#VBTPC get parent effective date
                            if ((twitterIssue.Type.Equals(TwitterIssueType.BTPC) ||
                                twitterIssue.Type.Equals(TwitterIssueType.VBTPC)) && !twitterIssue.EffectiveDate.HasValue)
                                twitterIssue.EffectiveDate = parent.EffectiveDate;

                        }
                        twitterIssueRepository.SaveEntity(twitterIssue);
                    }
                    else
                    {
                        log.Error(string.Format("The product with with ProductKey = {0} cannot be found",
                                                twitterIssue.ProductKey));
                    }
                }
                else //(twitterInDB != null)
                {
                    TwitterLifecycle.TwitterLifecycle lifecycle = TwitterLifecycle.TwitterLifecycle.Factory(twitterInDB);
                    TwitterIssue parent = lifecycle.FindParent();
                    bool isLeaf = lifecycle.IsLeaf;

                    if (parent != null && parent.Id.HasValue)
                    {
                        twitterInDB.PreviousTwittId = parent.Id.Value;
                        twitterInDB.Finished = isLeaf;
                        twitterIssueRepository.UpdateEntity(twitterInDB);

                    }
                }
            }
            catch (Exception ex)
            {
                 log.Error(string.Format("Error processing twitter, with TwitterInternalID ={0}",twitterIssue.TwitterId),ex);
            }
        }

        private Product FindProduct(string serviceToken)
        {
            ProductRepository productRepository = new ProductRepository();
            Product product = productRepository.FindByProductKey(serviceToken);
            if (product == null)
            {
                product = new Product()
                {
                    Hidden = false,
                    ProductKey = serviceToken,
                    ProductName = serviceToken,
                };
                product = productRepository.SaveEntity(product);
            }
            return product;

        }

        public IList<TwitterIssue> ProcessIssuesFromTweeter(int count)
        {
            IList<TwitterIssue> result = new List<TwitterIssue>();

            ITwittsProvider provider;
            if (string.IsNullOrEmpty(TwitterProviderClass))
            {
                provider = new TwittsProvider.TwittsProvider();
            }
            else
            {
                Type type = Assembly.GetExecutingAssembly().GetType(TwitterProviderClass);
                ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                provider = (ITwittsProvider)ctor.Invoke(null);
            }
            
            IList<NakedTwitt> nakedTwitts = provider.RetrieveTwitts(count);

            foreach (NakedTwitt item in nakedTwitts)
            {
                log.Info(string.Format("Assembling/Paring twitter ={0}", item.Text));
                TwitterIssue issue = AssembleTweetIssue(item);
                if (issue != null)
                    result.Add(issue);
            }
            return result;
        }

        private TwitterIssue AssembleTweetIssue(NakedTwitt nakedTwitt)
        {
            TwitterParser.TwitterParser parser = null;
            try
            {
                parser = TwitterParser.TwitterParser.Factory(nakedTwitt);

                return parser.Parse();
            }
            catch (Exception ex)
            {
                if (parser != null && parser.RegExpression != null)
                    log.Error(string.Format("Error parsing twitter: {0} with regex: {1}", nakedTwitt.Text, parser.RegExpression), ex);
                else
                    log.Error(string.Format("Error parsing twitter: {0}", nakedTwitt.Text), ex);
                    
                
                return null;
            }
        }


    }
}
