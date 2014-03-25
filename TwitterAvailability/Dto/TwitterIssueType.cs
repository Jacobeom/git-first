using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterAvailability.Dto
{
    public enum TwitterIssueType
    {
        TEFOP, 
        TEFCL, 
        BTP,
        BTPH,
        BTPC,
        BTPD,
        VBTPX,
        VBTPE,
        VBTPD,
        VBTPC
    }


    /*
     *  if (TypeString.Equals("#TEFOP"))
                    return TwitterIssueType.TEFOP;
                else if (TypeString.Equals("#TEFCL"))
                    return TwitterIssueType.TEFCL;
                else if (TypeString.Equals("#BTPH"))
                    return TwitterIssueType.BTP;
                else if (TypeString.Equals("#BTPC"))
                    return TwitterIssueType.BTPP;
                else if (TypeString.Equals("#BTPD"))
                    return TwitterIssueType.BTPC;
                else if (TypeString.Equals("#VBTPX"))
                    return TwitterIssueType.BTPC;
                else if (TypeString.Equals("#VBTPE"))
                    return TwitterIssueType.BTPC;
                else if (TypeString.Equals("#VBTPD"))
                    return TwitterIssueType.BTPC;
                else
                    return null;
     */
}
