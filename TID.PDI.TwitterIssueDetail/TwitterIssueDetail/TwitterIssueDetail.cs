using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using TwitterAvailability.Repository;

namespace TID.PDI.TwitterIssueDetail.TwitterIssueDetail
{
    [ToolboxItemAttribute(false)]
    public class TwitterIssueDetail : WebPart
    {
        private string _connectionString;
        private string _udoUrl;
   
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/TID.PDI.TwitterIssueDetail/TwitterIssueDetail/TwitterIssueDetailUserControl.ascx";

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
        WebDisplayName("UDOUrl")]
        public string UDOUrl
        {
            get { return _udoUrl; }
            set { _udoUrl = value; }
        }


        protected override void CreateChildControls()

        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                
                TwitterIssueDetailUserControl control = (TwitterIssueDetailUserControl)Page.LoadControl(_ascxPath);

                control.UDOUrl = UDOUrl;
                control.TwitterIssueRepository = new TwitterIssueRepository(_connectionString);
                control.IssueDetailRepository = new IssueDetailRepository(_connectionString);

                Controls.Add(control);
            }

           
        }
    }
}
