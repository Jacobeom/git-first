using System;
using System.Collections.Generic;
using TwitterAvailability.Dto;
using TwitterAvailability.Repository;

namespace TwitterAvailability.TwitterLifecycle
{
    public class VBTPELifeCycle : TwitterLifecycle
    {
        public VBTPELifeCycle(TwitterIssue twitterIssue)
            : base(twitterIssue)
        {
        }

        public override IList<string> ParentNodes
        {
            get { return new List<string>() { "#VBTPX", "#VBTPE" }; }
        }
        
        public override bool IsRoot
        {
            get { return false; }
        }
        public override bool IsLeaf
        {
            get { return false; }
        }
    }
}