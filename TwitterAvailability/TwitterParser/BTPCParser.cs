using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TwitterAvailability.Dto;

namespace TwitterAvailability.TwitterParser
{
    public class BTPCParser : TwitterParser
    {
        public BTPCParser(NakedTwitt nakedTwittToParse)
            : base(nakedTwittToParse)
        {
           
        }

        public override TwitterIssue Parse()
        {

            Match m = Match(nakedTwittToParse.Text);
            if (m == null)
            {
                //TODO:Log Error
                return null;
            }

            Func<String, String> all = (x) => x.Equals("*") ? "ALL" : x; 

            string twitterKeyToken = m.Groups["TwitterKeyToken"].Value;
            string commentToken = m.Groups["CommentToken"].Value;
            string serviceToken = m.Groups["ServiceToken"].Value;
            string countryToken = m.Groups["CountryToken"].Value;
            countryToken =all(countryToken);
            string idToken = m.Groups["IDToken"].Value;

            
            TwitterIssue result = new TwitterIssue();
            result.IntenalId = AssembleId(idToken,serviceToken,countryToken);
            result.Finished = false;
            result.CountryKey = countryToken;
            result.ProductKey = serviceToken;
            result.TypeString = twitterKeyToken.ToUpper();
            result.Description = commentToken;
            result.OriginalMessage = nakedTwittToParse.Text;
            result.TwitterDate = nakedTwittToParse.CreatedDate;
            result.TwitterId = nakedTwittToParse.StringId;
            result.PreviousTwittId = null;
            result.EstimatedMinutes = null;
            result.StartingDate = null;
            result.EndingDate = null;
            //TODO: Effective Date will be set whenever the parent twitt is setted
            result.EffectiveDate = null;
            result.ProductId = -1;

            return result;

        }

        public override string RegExpression
        {
            get { return @"(?<TwitterKeyToken>\#BTPC)\s+(?<ServiceToken>\w{1,3})\.(?<CountryToken>\w{1,2}|\*)\s+\((?<IDToken>.+)\)\s+(?<CommentToken>.+)"; }
        }
    }
}
