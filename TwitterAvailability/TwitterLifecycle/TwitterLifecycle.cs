using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using TwitterAvailability.Dto;
using TwitterAvailability.Repository;

namespace TwitterAvailability.TwitterLifecycle
{
    public abstract class TwitterLifecycle
    {
        private static ILog log = LogManager.GetLogger(typeof(TwitterLifecycle));

        public TwitterLifecycle(TwitterIssue twitterIssue)
        {
            this.twitterIssue = twitterIssue;
        }


        protected TwitterIssue twitterIssue;

      

        public abstract IList<string> ParentNodes { get; }
        public abstract bool IsRoot { get; }
        public abstract bool IsLeaf { get; }


        public virtual TwitterIssue FindParent()
        {
            if (IsRoot)
            {
                return null;
            }
            else
            {
                TwitterIssueRepository twitterIssueRepository = new TwitterIssueRepository();

                if (twitterIssue.PreviousTwittId.HasValue)
                {
                    return twitterIssueRepository.FindByUniqueId(twitterIssue.PreviousTwittId.Value);
                }
                else
                {
                    return twitterIssueRepository.FindByInternalIdAndTypeOrderByTwitterDate(twitterIssue.TwitterDate, twitterIssue.IntenalId, ParentNodes);
                }
            }
        }


        public static TwitterLifecycle Factory(TwitterIssue twitterIssue)
        {
          
            if (twitterIssue.Type.HasValue)
            {
                switch (twitterIssue.Type.Value)
                {
                     case TwitterIssueType.BTP:
                        return new BTPLifeCycle(twitterIssue);
                        break;
                     case TwitterIssueType.TEFOP:
                        return new TEFOPLifeCycle(twitterIssue);
                        break;
                     case TwitterIssueType.BTPH:
                        return new BTPHLifeCycle(twitterIssue);
                        break;
                     case TwitterIssueType.VBTPX:
                        return new VBTPXLifeCycle(twitterIssue);
                        break;
                     case TwitterIssueType.VBTPE:
                        return new VBTPELifeCycle(twitterIssue);
                        break;
                     case TwitterIssueType.VBTPD:
                        return new VBTPDLifeCycle(twitterIssue);
                        break;
                     case TwitterIssueType.VBTPC:
                        return new VBTPCLifeCycle(twitterIssue);
                        break;
                     case TwitterIssueType.TEFCL:
                        return new TEFCLLifeCycle(twitterIssue);
                        break;
                     case TwitterIssueType.BTPC:
                        return new BTPCLifeCycle(twitterIssue);
                        break;
                     case TwitterIssueType.BTPD:
                        return new BTPDLifeCycle(twitterIssue);
                        break;
                    default:
                       log.Error(string.Format("TwitterLifecyle cannot deal with {0}",twitterIssue.TypeString));
                        return null;
                        break;
                }
            }
            else
            {
                log.Error(string.Format("TwitterLifecyle cannot deal with {0}",twitterIssue.TypeString));
                return null;
            }
        }

    }
}
