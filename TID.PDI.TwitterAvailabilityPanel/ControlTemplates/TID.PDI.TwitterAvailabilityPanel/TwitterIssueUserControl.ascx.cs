using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using TwitterAvailability.Dto;
using System.Linq;

namespace TID.PDI.TwitterAvailabilityPanel.ControlTemplates.TID.PDI.TwitterAvailabilityPanel
{
    public partial class TwitterIssueUserControl : UserControl
    {
        private Product twitterIssue;

        private IList<DateTime> dates;
        private Product product;
        private IList<TwitterAvailability.Dto.TwitterIssue> leafTwitterIssues;

        public bool isOdd { get; set; }
        public IList<DateTime> Dates { set { dates = value; } }
        public Product Product { get { return product; } set { product = value; } }
        public IList<TwitterIssue> LeafTwitterIssues { set { leafTwitterIssues = value; } }

        public string BackgroundColor(DateTime date)
        {
            if (date.DayOfWeek.Equals(DayOfWeek.Saturday) || date.DayOfWeek.Equals(DayOfWeek.Sunday))
                return "rgb(238 , 236, 225)";
            else
                return isOdd ? "#E8F6F9" : "white";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            this.TwitterIssueRepeater.DataSource = dates;
            this.TwitterIssueRepeater.DataBind();
        }

        protected void TwitterIssueRepeaterItemBound(object sender, RepeaterItemEventArgs args)
        {
           
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                DateTime datePlain = (DateTime)args.Item.DataItem;

                IList<TwitterIssue> tweetsofToday =
                    leafTwitterIssues.Where(
                        x => x.ProductId.Equals(product.Id) && x.EffectiveDate.Value.Year.Equals(datePlain.Date.Year)
                             && x.EffectiveDate.Value.Month.Equals(datePlain.Date.Month) &&
                             x.EffectiveDate.Value.Day.Equals(datePlain.Date.Day)).ToList();

                PlaceHolder btpPh = (PlaceHolder)args.Item.FindControl("BTPPlaceHolder");
                PlaceHolder tefopPh = (PlaceHolder)args.Item.FindControl("TEFOPPlaceHolder");
                PlaceHolder vbtpxPh = (PlaceHolder)args.Item.FindControl("VBTPXPlaceHolder");
                

                foreach (TwitterAvailability.Dto.TwitterIssue issue in tweetsofToday)
                {
                    Image image = new Image();
                    image.ImageUrl = String.Format("{0}/Style Library/TID.PDI.TwitterAvailabilityPanel/Styles/Images/{1}.png", SPContext.Current.Site.Url, issue.Type.Value.ToString());
                    image.Attributes.Add("twittIssueId", issue.Id.Value.ToString());
                    string tag;
                    if (issue.StartingDate.HasValue && issue.EndingDate.HasValue)
                        tag = string.Format("{0} - {1} [{2} - {3}]", issue.TypeString, issue.Description,
                                            issue.StartingDate.Value.ToString("dd/MM/yyyy hh:mm"),
                                            issue.EndingDate.Value.ToString("dd/MM/yyyy hh:mm"));
                    else if (issue.StartingDate.HasValue)
                        tag = string.Format("{0} - {1} [{2}]", issue.TypeString, issue.Description,
                                issue.StartingDate.Value.ToString("dd/MM/yyyy hh:mm"));
                    else if (issue.EndingDate.HasValue)
                        tag = string.Format("{0} - {1} [{2}]", issue.TypeString, issue.Description,
                              issue.EndingDate.Value.ToString("dd/MM/yyyy hh:mm"));
                    else
                        tag = string.Format("{0} - {1}", issue.TypeString, issue.Description);

                    image.AlternateText = tag;
                    image.CssClass = "TwitterIssueImage";

                    if (issue.Type.Equals(TwitterIssueType.TEFOP) || issue.Type.Equals(TwitterIssueType.TEFCL))
                    {
                        tefopPh.Controls.Add(image);
                    }
                    else if (issue.Type.Equals(TwitterIssueType.BTP) || issue.Type.Equals(TwitterIssueType.BTPH) ||
                        issue.Type.Equals(TwitterIssueType.TEFOP) || issue.Type.Equals(TwitterIssueType.BTPD))
                    {
                        btpPh.Controls.Add(image);
                    }
                    else
                    {
                        vbtpxPh.Controls.Add(image);
                    }

                }
            }
        }
    }
}