using System;
using System.Collections.Generic;
using TwitterAvailability.Dto;
using TwitterAvailability.Repository;

namespace TwitterAvailability.TwitterLifecycle
{
    public class TEFCLLifeCycle : TwitterLifecycle
    {
        public TEFCLLifeCycle(TwitterIssue twitterIssue)
            : base(twitterIssue)
        {
        }

        public override TwitterIssue FindParent()
        {
            TwitterIssueRepository twitterIssueRepository = new TwitterIssueRepository();

            if (twitterIssue.PreviousTwittId.HasValue)
            {
                return twitterIssueRepository.FindByUniqueId(twitterIssue.PreviousTwittId.Value);
            }
            else
            {
                if (twitterIssue.EffectiveDate.HasValue)
                    return
                        twitterIssueRepository.FindByInternalIdAndTypeOrderByEffectiveDate(
                            twitterIssue.EffectiveDate.Value, twitterIssue.IntenalId, ParentNodes);
                else
                    throw new Exception(
                       String.Format("The TEFCL issue with InternalId: {0}, doesn't contains effective date so his parent cannot be found",
                                      twitterIssue.IntenalId));
                    

            }
        }

        public override IList<string> ParentNodes
        {
            get { return new List<string>() { "#TEFOP" }; }
        }

        public override bool IsRoot
        {
            get { return false; }
        }
        public override bool IsLeaf
        {
            get { return true; }
        }
    }
}