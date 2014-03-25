using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterAvailability.Dto;

namespace TwitterAvailability.TwitterParser
{
    public class BTPHParser : BTPParser
    {
        public BTPHParser(NakedTwitt nakedTwittToParse)
            : base(nakedTwittToParse)
        {
           
        }

        public override string RegExpression
        {
            get { return @"(?<TwitterKeyToken>\#BTPH)\s+(?<ServiceToken>\w{1,3})\.(?<CountryToken>\w{1,2}|\*)\s+(?<StartingDateToken>\d{4}:\d{2}:\d{2}:\d{2}:\d{2})\s+(?<EstimatedMinutesToken>\d+)\s+\((?<IDToken>.+)\)\s+(?<CommentToken>.+)"; }
        }
    }
}
