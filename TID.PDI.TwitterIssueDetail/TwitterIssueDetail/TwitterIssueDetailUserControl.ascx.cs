using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TwitterAvailability.Dto;
using TwitterAvailability.Repository;

namespace TID.PDI.TwitterIssueDetail.TwitterIssueDetail
{
    public partial class TwitterIssueDetailUserControl : UserControl
    {

        public TwitterIssueRepository TwitterIssueRepository { get; set; }
        public IssueDetailRepository IssueDetailRepository { get; set; }
        public String  UDOUrl { get; set; }

        private IList<TwitterIssue> _historyTwitterIssues;
        public IList<TwitterIssue> HistoryTwitterIssues
        {
            get
            {
                if (_historyTwitterIssues == null)
                {
                    
                    _historyTwitterIssues = TwitterIssueRepository.FindAllParentNodesFromThis(twitterIssueId);
                }
                return _historyTwitterIssues;
            }
        }

        private IList<TwitterAvailability.Dto.IssueDetail> _issueDetails;
        private IList<TwitterAvailability.Dto.IssueDetail> IssueDetails
        {
            get
            {
                if (_issueDetails == null)
                {
                    _issueDetails = IssueDetailRepository.FindAllByInternalId(HistoryTwitterIssues[0].IntenalId);
                }
                return _issueDetails;
            }
        }

        private int twitterIssueId;

        protected void Page_Load(object sender, EventArgs e)
        {
             
            if (string.IsNullOrEmpty(this.Context.Request.Params["twitterIssueId"]) ||
                !int.TryParse(this.Context.Request.Params["twitterIssueId"], out twitterIssueId))
                
            {
                error_p.Visible = true;
            }
            else
            {
                error_p.Visible = false;
                twitterIssueId = int.Parse(this.Context.Request.Params["twitterIssueId"]);
                TwitterHistoryRepeater.DataSource = HistoryTwitterIssues;
                TwitterHistoryRepeater.DataBind();

                IssuesDetailsRepeater.DataSource = IssueDetails;
                IssuesDetailsRepeater.DataBind();
            }
        }

        protected override void OnError(EventArgs e)
        {
            error_p.Visible = true;
        }

        protected void AddItem(object sender, EventArgs e)
        {
            string pattern = @"dd/MM/yyyy H:m";
            IssueDetail twitterIssueDetail = new IssueDetail();
            
            
            twitterIssueDetail.Date =System.DateTime.ParseExact(DateTime.Text,pattern,null);;
            twitterIssueDetail.Description =this.Description.Text;
            twitterIssueDetail.InternalId = HistoryTwitterIssues[0].IntenalId;

            if (FileUpload.HasFile)
            {
                twitterIssueDetail.FileName = FileUpload.PostedFile.FileName;
                twitterIssueDetail.ContentType = FileUpload.PostedFile.ContentType;

                Stream fs = FileUpload.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                twitterIssueDetail.Data = br.ReadBytes((Int32)fs.Length);
            }

            IssueDetailRepository.SaveEntity(twitterIssueDetail);
            _issueDetails = null;
            IssuesDetailsRepeater.DataSource = IssueDetails;
            IssuesDetailsRepeater.DataBind();

        }

        protected void IssueDetailsRepeaterItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            IssueDetail issueDetail = IssueDetailRepository.FindByUniqueId(id);

            if (e.CommandName.Equals("Download"))
            {
                Byte[] issueDetailData = IssueDetailRepository.GetDataFromUniqueId(id);


                HttpResponse response = this.Context.Response;
                response.Buffer = true;
                response.Charset = string.Empty;
                response.Cache.SetCacheability(HttpCacheability.NoCache);
                response.ContentType = issueDetail.ContentType;
                response.AddHeader("content-disposition", "attachment;filename=" + issueDetail.FileName);
                response.BinaryWrite(issueDetailData);
                response.Flush();
                response.End();
            }
            if (e.CommandName.Equals("Delete"))
            {
                IssueDetailRepository.DeleteEntity(id);
                _issueDetails = null;
                IssuesDetailsRepeater.DataSource = IssueDetails;
                IssuesDetailsRepeater.DataBind();
            }

        }

    }
}
