using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TwitterAvailability.Dto;

namespace TwitterAvailability.TwitterParser
{
    public class BTPDParser : TEFCLParser
    {
        public BTPDParser(NakedTwitt nakedTwittToParse)
            : base(nakedTwittToParse)
        {
            
        }

         public override string RegExpression
        {
            get { return @"(?<TwitterKeyToken>\#BTPD)\s+(?<ServiceToken>\w{1,3})\.(?<CountryToken>\w{1,2}|\*)\s+(?<EndingDateToken>\d{4}:\d{2}:\d{2}:\d{2}:\d{2})\s+\((?<IDToken>.+)\)\s+(?<CommentToken>.+)"; }
        }
    }
    
}