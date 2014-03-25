using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using TID.PDI.TwitterAvailabilityPanel.ControlTemplates.TID.PDI.TwitterAvailabilityPanel;
using TwitterAvailability.Dto;
using TwitterAvailability.Repository;

namespace TID.PDI.TwitterAvailabilityPanel.AvailabilityPanel
{
    public partial class AvailabilityPanelUserControl : UserControl
    {
        public TwitterIssueRepository TwitterIssueRepository { get; set; }
        public ProductRepository ProductRepository { get; set; }
        public int NumberOfWeeksShown { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        private IList<TwitterAvailability.Dto.TwitterIssue> _leafTwitterIssues;

        private IList<TwitterAvailability.Dto.TwitterIssue> LeafTwitterIssues
        {
            get
            {
                if (_leafTwitterIssues==null)
                {
                    //TODO: just get last Month
                    DateTime from =  DateTime.Now.AddDays(-7*NumberOfWeeksShown);
                    DateTime to = DateTime.Now; 
                    _leafTwitterIssues = TwitterIssueRepository.FindAllLeafNodesRightFormed(from,to);
                }
                return _leafTwitterIssues;
            }
        }

        private IList<TwitterAvailability.Dto.Product> _products;
        private IList<TwitterAvailability.Dto.Product> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = ProductRepository.FindAll();
                }
                return _products;
            }
        }

        public string TwitterIssueTypeUrl(TwitterIssueType type)
        {
           return String.Format("{0}/Style Library/TID.PDI.TwitterAvailabilityPanel/Styles/Images/{1}.png", SPContext.Current.Site.Url, type.ToString());
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            ((TwitterFeed) twitterFeed).AccessToken = AccessToken;
            ((TwitterFeed)twitterFeed).AccessTokenSecret = AccessTokenSecret;
            ((TwitterFeed)twitterFeed).ConsumerKey = ConsumerKey;
            ((TwitterFeed)twitterFeed).ConsumerSecret = ConsumerSecret;


            productsRepeater.DataSource = Products;
            productsRepeater.DataBind();

            datesRepeater.DataSource = Dates();
            datesRepeater.DataBind();

            productDatesRepeater.DataSource = Products;
            productDatesRepeater.DataBind();

          
        }

        protected void DatesRepeaterItemBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {

                PlaceHolder ph = (PlaceHolder)args.Item.FindControl("DatesRepeaterPlaceHolder");
                TwitterIssueUserControl milestoneControl = (TwitterIssueUserControl)Page.LoadControl("~/_controltemplates/TID.PDI.TwitterAvailabilityPanel/TwitterIssueUserControl.ascx");
                milestoneControl.Dates = Dates();
                milestoneControl.LeafTwitterIssues = LeafTwitterIssues;
                milestoneControl.isOdd = args.Item.ItemType == ListItemType.Item;
                milestoneControl.Product = (Product)args.Item.DataItem;
                ph.Controls.Add(milestoneControl);
            }
        }
   
        public  IList<DateTime> Dates()
        {
            IList<DateTime> result = new List<DateTime>();
            int days = 7 * NumberOfWeeksShown *-1;
            DateTime from = DateTime.Now.AddDays(days);
            DateTime to = DateTime.Now;

            //TODO: just get last Month
            for (DateTime i = from; i <= to; i= i.AddDays(+1))
            {
                result.Add(i);
            }
            return result;
       }

    }   
}
