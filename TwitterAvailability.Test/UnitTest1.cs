using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterAvailability.Dto;
using TwitterAvailability.Repository;
using TwitterAvailability.Service;
using Twitterizer;
using System.Linq;
using log4net.Config;

namespace TwitterAvailability.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string accessToken = ConfigurationManager.AppSettings["Twitter.Tokens.AccessToken"];
            string accessTokenSecret = ConfigurationManager.AppSettings["Twitter.Tokens.AccessTokenSecret"];
            string accessTokenKey = ConfigurationManager.AppSettings["Twitter.Tokens.ConsumerKey"];
            string accessPassword = ConfigurationManager.AppSettings["Twitter.Tokens.ConsumerSecret"];

           OAuthTokens tokens = new OAuthTokens()
                                    {
                                        AccessToken = accessToken,
                                        AccessTokenSecret = accessTokenSecret,
                                        ConsumerKey = accessTokenKey,
                                        ConsumerSecret = accessPassword
                                    };
                                  
            TimelineOptions timelineOptions = new TimelineOptions()
                                                  {
                                                      Count = 200,
                                                      IncludeRetweets = true
                                                  };


            TwitterResponse<TwitterStatusCollection> homeTimeline = TwitterTimeline.HomeTimeline(tokens,timelineOptions);
            

            TwitterStatusCollection collection =homeTimeline.ResponseObject;


        }

       

         
        [TestMethod]
        public void insertProduct()
        {
            ProductRepository productRepository = new ProductRepository();

            Product product = new Product()
                                  {
                                      Hidden = false,
                                      ProductName = "my name",
                                      ProductKey = "key",
                                  };

            productRepository.SaveEntity(product);
        }

        [TestMethod]
        public void selectProduct()
        {
            
            TwitterIssueRepository issueRepository = new TwitterIssueRepository();
            IList<TwitterIssue> res = issueRepository.FindAllParentNodesFromThis(30);
            //TwitterIssue result = issueRepository.FindByInternalIdAndTypeOrderByTwitterDate("theID", new List<string>() {"#BTP", "#BTPC"});
            Int16 a = 23;
        }
           

        [TestMethod]
        public void Run()
        {


            XmlConfigurator.Configure();

            TwitterIssueRepository twitterIssueRepository = new TwitterIssueRepository();

            DateTime from = DateTime.Now.AddMonths(-2);
            DateTime to = DateTime.Now;
            

            var _leafTwitterIssues = twitterIssueRepository.FindAllLeafNodesRightFormed(from, to);


            ////return;
            //TwitterService service = new TwitterService();

            //service.Run();
        }


        [TestMethod]
        public void ParseDate()
        {
            try
            {
                string pattern = @"yyyy:MM:dd:HH:mm";

                string date = @"2012:14:02:22:55";
                DateTime result;
                DateTime a = DateTime.ParseExact(date, pattern, null, DateTimeStyles.AssumeUniversal);
            }
            catch (Exception e)
            {
              //Logger.Write();
            }
        } 

        [TestMethod]
        public void RegEx()
        {
            string pattern = @"(?<TwitterKeyToken>\#TEFOP)\s+(?<ServiceToken>\w{1,3})\.(?<CountryToken>\w{1,2}|\*)\s+(?<StartingDateToken>\d{4}:\d{2}:\d{2}:\d{2}:\d{2})\s+\((?<IDToken>.+)\)\s+(?<CommentToken>.+)";
            string sample = @"#TEFOP 1BV.ES 2012:01:01:15:30 (myId) Pues este es el maldito regex";


            MatchCollection matches = Regex.Matches(sample, pattern,
            RegexOptions.ExplicitCapture| RegexOptions.Singleline  |RegexOptions.IgnoreCase);

            foreach (Match m in matches)
            {
                // note that EITHER fieldtoken OR DataToken will have a value in each loop
                string twitterKeyToken = m.Groups["TwitterKeyToken"].Value;
                string commentToken = m.Groups["CommentToken"].Value;
                string serviceToken = m.Groups["ServiceToken"].Value;
                string countryToken = m.Groups["CountryToken"].Value;
                string startingDateToken = m.Groups["StartingDateToken"].Value;
                string idToken = m.Groups["IDToken"].Value;
                
                

            }      
        }

        [TestMethod]
        public void RegEx2()
        {
            string pattern = @"(?<TwitterKeyToken>\#TEFOP)\s+(?<ServiceToken>\w{1,3})\.(?<CountryToken>\w{1,2}|\*)\s+(?<StartingDateToken>\d{4}:\d{2}:\d{2}:\d{2}:\d{2})\s+(?<EstimatedMinutesToken>\d+)\s+\((?<IDToken>.+)\)\s+(?<CommentToken>.+)";
            string sample = @"#TEFOP ABV.ES 2012:01:01:15:30 22 (myId) Pues este es el maldito regex";


            MatchCollection matches = Regex.Matches(sample, pattern,
            RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match m in matches)
            {
                // note that EITHER fieldtoken OR DataToken will have a value in each loop
                string twitterKeyToken = m.Groups["TwitterKeyToken"].Value;
                string commentToken = m.Groups["CommentToken"].Value;
                string serviceToken = m.Groups["ServiceToken"].Value;
                string countryToken = m.Groups["CountryToken"].Value;
                string startingDateToken = m.Groups["StartingDateToken"].Value;
                string idToken = m.Groups["IDToken"].Value;
                string estimatedMinutesToken = m.Groups["EstimatedMinutesToken"].Value;



            }
        }



    }
    
}
