using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using TwitterAvailability.Repository;

namespace TID.PDI.TwitterAvailabilityPanel.AvailabilityPanel
{
    [ToolboxItemAttribute(false)]
    public class AvailabilityPanel : WebPart
    {
        private String _connectionString;
        private int numberOfWeeksShown=4;
        private String _accessToken;
        private String _accessTokenSecret;
        private String _consumerKey;
        private String _consumerSecret;
        

        [Category("TID"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("ConnectionString")]
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        [Category("TID"),
         Personalizable(PersonalizationScope.Shared),
         WebBrowsable(true),
         WebDisplayName("Number of Weeks Shown")]
        public int NumberOfWeeksShown
        {
            get { return numberOfWeeksShown; }
            set { numberOfWeeksShown = value; }
        }


        [Category("TID"),
           Personalizable(PersonalizationScope.Shared),
           WebBrowsable(true),
           WebDisplayName("AccessToken")]
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        [Category("TID"),
          Personalizable(PersonalizationScope.Shared),
          WebBrowsable(true),
          WebDisplayName("AccessTokenSecret")]
        public string AccessTokenSecret
        {
            get { return _accessTokenSecret; }
            set { _accessTokenSecret = value; }
        }

        [Category("TID"),
          Personalizable(PersonalizationScope.Shared),
          WebBrowsable(true),
          WebDisplayName("ConsumerKey")]
        public string ConsumerKey
        {
            get { return _consumerKey; }
            set { _consumerKey = value; }
        }


        [Category("TID"),
          Personalizable(PersonalizationScope.Shared),
          WebBrowsable(true),
          WebDisplayName("ConsumerSecret")]
        public string ConsumerSecret
        {
            get { return _consumerSecret; }
            set { _consumerSecret = value; }
        }



        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/TID.PDI.TwitterAvailabilityPanel/AvailabilityPanel/AvailabilityPanelUserControl.ascx";

        protected override void CreateChildControls()
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                AvailabilityPanelUserControl control = (AvailabilityPanelUserControl) Page.LoadControl(_ascxPath);

                control.TwitterIssueRepository = new TwitterIssueRepository(_connectionString);
                control.ProductRepository = new ProductRepository(_connectionString);
                control.NumberOfWeeksShown = numberOfWeeksShown;
                control.AccessToken = AccessToken;
                control.AccessTokenSecret = AccessTokenSecret;
                control.ConsumerKey = ConsumerKey;
                control.ConsumerSecret = ConsumerSecret;
                Controls.Add(control);
            }          
        }
    }
}
