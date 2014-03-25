using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Common.Logging;
using TwitterAvailability.Dto;
using Twitterizer;

namespace TwitterAvailability.TwittsProvider
{
    public class TwittsProvider: ITwittsProvider
    {
        ILog log = LogManager.GetLogger(typeof(TwittsProvider));

        public string AccessToken
        {
            get { return ConfigurationManager.AppSettings["Twitter.Tokens.AccessToken"]; }
        }

        public string AccessTokenSecret
        {
            get { return ConfigurationManager.AppSettings["Twitter.Tokens.AccessTokenSecret"]; }
        }

        public string ConsumerKey
        {
            get { return ConfigurationManager.AppSettings["Twitter.Tokens.ConsumerKey"]; }
        }

        public string ConsumerSecret
        {
            get { return ConfigurationManager.AppSettings["Twitter.Tokens.ConsumerSecret"]; }
        }



        public IList<NakedTwitt> RetrieveTwitts(int count)
        {
            List<NakedTwitt> result = new List<NakedTwitt>();

            try
            {
                OAuthTokens tokens = new OAuthTokens()
                                         {
                                             AccessToken = AccessToken,
                                             AccessTokenSecret = AccessTokenSecret,
                                             ConsumerKey = ConsumerKey,
                                             ConsumerSecret = ConsumerSecret
                                         };

                TimelineOptions timelineOptions = new TimelineOptions()
                                                      {
                                                          Count = count,
                                                          IncludeRetweets = false
                                                      };

                TwitterResponse<TwitterStatusCollection> homeTimeline = TwitterTimeline.HomeTimeline(tokens,
                                                                                                     timelineOptions);
                result =
                    homeTimeline.ResponseObject.Select(
                        x => new NakedTwitt() {StringId = x.StringId, Text = x.Text, CreatedDate = x.CreatedDate})
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error retrieving twitts from the internet", ex);
                return result;
            }
        }
    }
}
