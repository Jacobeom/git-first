using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using TID.PDI.TwitterAvailabilityPanel.ControlTemplates.TID.PDI.TwitterAvailabilityPanel;
using Twitterizer;

namespace TID.PDI.TwitterAvailabilityPanel.ControlTemplates.TID.PDI.TwitterAvailabilityPanel
{
    public class TweeterDto
    {
        public String Text { get; set; }
        public DateTime CreatedDate { get; set; }

    }
    public partial class TwitterFeed : UserControl
    {

        public string AccessToken {get; set; }
        public string AccessTokenSecret {get; set; }
        public string ConsumerKey {get; set; }
        public string ConsumerSecret {get; set; }


        public string TwitterImage
        {
            get
            {
              return String.Format("{0}/Style Library/TID.PDI.TwitterAvailabilityPanel/Styles/Images/logo-twitter.png", SPContext.Current.Site.Url);  
            }
        }

        private OAuthTokens Tokens
        {
            get
            {
                OAuthTokens tokens = new OAuthTokens();

                tokens.AccessToken = AccessToken;
                tokens.AccessTokenSecret = AccessTokenSecret;
                tokens.ConsumerKey = ConsumerKey;
                tokens.ConsumerSecret = ConsumerSecret;

                return tokens;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TwitterTimeline.HomeTimeline(Tokens)!= null  ){
                TwitterResponse<TwitterStatusCollection> homeTimeline = TwitterTimeline.HomeTimeline(Tokens);
                if (homeTimeline != null && homeTimeline.ResponseObject != null) { 

                    List<TweeterDto> tweets =
                        homeTimeline.ResponseObject.Select(x => new TweeterDto() { Text = x.Text, CreatedDate = x.CreatedDate }).Take(40).ToList();


                    tweetsRepeater.DataSource = tweets;
                    tweetsRepeater.DataBind();
                }
            }
        }
    }

}