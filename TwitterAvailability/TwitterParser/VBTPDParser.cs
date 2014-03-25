using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterAvailability.Dto;

namespace TwitterAvailability.TwitterParser
{
    public class VBTPDParser: TEFCLParser
    {
        public VBTPDParser(NakedTwitt nakedTwittToParse)
            : base(nakedTwittToParse)
        {
           
        }
        public override string RegExpression
        {
            get { return @"(?<TwitterKeyToken>\#VBTPD)\s+(?<ServiceToken>\w{1,3})\.(?<CountryToken>\w{1,2}|\*)\s+(?<EndingDateToken>\d{4}:\d{2}:\d{2}:\d{2}:\d{2})\s+\((?<IDToken>.+)\)\s+(?<CommentToken>.+)"; }
        }

    }
}