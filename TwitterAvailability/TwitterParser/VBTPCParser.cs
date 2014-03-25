using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterAvailability.Dto;

namespace TwitterAvailability.TwitterParser
{
    public class VBTPCParser: BTPCParser
    {
        public VBTPCParser(NakedTwitt nakedTwittToParse)
            : base(nakedTwittToParse)
        {
           
        }

        public override string RegExpression
        {
            get { return @"(?<TwitterKeyToken>\#VBTPC)\s+(?<ServiceToken>\w{1,3})\.(?<CountryToken>\w{1,2}|\*)\s+\((?<IDToken>.+)\)\s+(?<CommentToken>.+)"; }
        }
    }
}
