using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterAvailability.Dto
{
    public class TwitterIssue
    {
        public int? Id { get; set; }
        public String TwitterId { get; set; }
        public String IntenalId { get; set; }
        public String TypeString { get; set; }
        public String ProductKey { get; set; }
        public String CountryKey { get; set; }
        public String Description { get; set; }

        public String OriginalMessage { get; set; }

      
        public DateTime TwitterDate { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? EstimatedMinutes { get; set; }


        public int ProductId { get; set; }
        public bool Finished { get; set; }


        public int? PreviousTwittId { get; set; }

        public TwitterIssueType? Type
        {
            get
            {
                if (TypeString.Equals("#TEFOP"))
                    return TwitterIssueType.TEFOP;
                else if (TypeString.Equals("#TEFCL"))
                    return TwitterIssueType.TEFCL;
                else if (TypeString.Equals("#BTP"))
                    return TwitterIssueType.BTP;
                else if (TypeString.Equals("#BTPH"))
                    return TwitterIssueType.BTPH;
                else if (TypeString.Equals("#BTPD"))
                    return TwitterIssueType.BTPD;
                else if (TypeString.Equals("#BTPC"))
                    return TwitterIssueType.BTPC;
                else if (TypeString.Equals("#VBTPX"))
                    return TwitterIssueType.VBTPX;
                else if (TypeString.Equals("#VBTPE"))
                    return TwitterIssueType.VBTPE;
                else if (TypeString.Equals("#VBTPD"))
                    return TwitterIssueType.VBTPD;
                else if (TypeString.Equals("#VBTPC"))
                    return TwitterIssueType.VBTPC;
                else
                    return null;
            }
        }
    }
}
