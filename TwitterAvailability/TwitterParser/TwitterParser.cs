using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Common.Logging;
using TwitterAvailability.Dto;
using TwitterAvailability.Repository;

namespace TwitterAvailability.TwitterParser
{
    public abstract class TwitterParser
    {
        protected NakedTwitt nakedTwittToParse;

        private static ILog log = LogManager.GetLogger(typeof(TwitterParser));

        protected TwitterParser(NakedTwitt nakedTwittToParse)
        {
            this.nakedTwittToParse = nakedTwittToParse;
        }

        public static TwitterParser Factory(NakedTwitt nakedTwittToParse)
        {
             string regExpression = @"(?<TwitterKeyToken>\#\w+)\s+.+";

             MatchCollection matches = Regex.Matches(nakedTwittToParse.Text, regExpression,
                RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            string twiterKeyToken = string.Empty;

            if (matches.Count > 0 &&
                matches[0].Groups["TwitterKeyToken"] != null &&
                !string.IsNullOrEmpty(matches[0].Groups["TwitterKeyToken"].Value))
            {
                twiterKeyToken = matches[0].Groups["TwitterKeyToken"].Value;
                switch (twiterKeyToken.ToUpper())
                {
                    case "#TEFOP":
                        return new TEFOPParser(nakedTwittToParse);
                        break;
                    case "#TEFCL":
                        return new TEFCLParser(nakedTwittToParse);
                        break;
                    case "#BTP":
                        return new BTPParser(nakedTwittToParse);
                        break;
                    case "#BTPH":
                        return new BTPHParser(nakedTwittToParse);
                        break;
                    case "#BTPC":
                        return new BTPCParser(nakedTwittToParse);
                        break;
                    case "#BTPD":
                        return new BTPDParser(nakedTwittToParse);
                        break;
                    case "#VBTPX":
                        return new VBTPXParser(nakedTwittToParse);
                        break;
                    case "#VBTPE":
                        return new VBTPEParser(nakedTwittToParse);
                        break;
                    case "#VBTPD":
                        return new VBTPDParser(nakedTwittToParse);
                        break;
                    case "#VBTPC":
                        return new VBTPCParser(nakedTwittToParse);
                        break;
                    default:
                        log.Error(string.Format("TwitterParser cannot deal with {0}",twiterKeyToken));
                        return null;
                        break;
                }
            }
            else
            {
                log.Error(string.Format("TwitterParser cannot deal with {0}", nakedTwittToParse.Text));
                return null;
            }
        }

        protected DateTime? ParseDate(string dateToParse)
        {
            try
            {
                string pattern = @"yyyy:MM:dd:HH:mm";
                DateTime result = DateTime.ParseExact(dateToParse, pattern, null);
                return result;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Date {0} cannnot be parsed, twittId={1}", dateToParse, nakedTwittToParse.Text), ex);
                return null;
            } 
        } 

        protected string AssembleId(String internalId, String serviceId, String countryId)
        {
            return string.Format("{0}.{1}.{2}", internalId, serviceId, countryId);
        }

        protected Match Match(string toParse)
        {
            MatchCollection matches = Regex.Matches(toParse, RegExpression,
                 RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (matches.Count > 0)
                return matches[0];
            else
            {
                log.Error(string.Format("TwitterParser cannot match {0}", nakedTwittToParse.Text));
                return null;
            }
              

        }

        public abstract TwitterIssue Parse();

        public abstract string RegExpression { get; }
    }
}
