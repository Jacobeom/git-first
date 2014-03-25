using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterAvailability.Dto;

namespace TwitterAvailability.TwitterLifecycle
{
    public class TEFOPLifeCycle : TwitterLifecycle
    {
        public TEFOPLifeCycle(TwitterIssue twitterIssue) : base(twitterIssue)
        {
        }

        public override IList<string> ParentNodes
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsRoot
        {
            get { return true; }
        }
       
        public override bool IsLeaf
        {
            get { return false; }
        }
    }

    public class BTPLifeCycle : TwitterLifecycle
    {
        public BTPLifeCycle(TwitterIssue twitterIssue)
            : base(twitterIssue)
        {
           
        }

        public override IList<string> ParentNodes
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsRoot
        {
            get { return true; }
        }

        public override bool IsLeaf
        {
            get { return false; }
        }
    }

    public class BTPHLifeCycle : TwitterLifecycle
    {
        public BTPHLifeCycle(TwitterIssue twitterIssue)
            : base(twitterIssue)
        {
        }

        public override IList<string> ParentNodes
        {
            get { return new List<string>() { "#BTPH", "#BTP" }; }
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

    public class VBTPXLifeCycle : TwitterLifecycle
    {
        public VBTPXLifeCycle(TwitterIssue twitterIssue)
            : base(twitterIssue)
        {
        }

        public override IList<string> ParentNodes
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsRoot
        {
            get { return true; }
        }
        public override bool IsLeaf
        {
            get { return false; }
        }
    }

    public class VBTPDLifeCycle : TwitterLifecycle
    {
          public VBTPDLifeCycle(TwitterIssue twitterIssue)
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
            get { return true; }
        }
    }

    public class VBTPCLifeCycle : TwitterLifecycle
    {
        public VBTPCLifeCycle(TwitterIssue twitterIssue)
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
            get { return true; }
        }
    }

    public class BTPCLifeCycle : TwitterLifecycle
    {
          public BTPCLifeCycle(TwitterIssue twitterIssue)
            : base(twitterIssue)
        {
        }

        public override IList<string> ParentNodes
        {
            get { return new List<string>() { "#BTPH", "#BTP" }; }
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

    public class BTPDLifeCycle : TwitterLifecycle
    {
        public BTPDLifeCycle(TwitterIssue twitterIssue)
            : base(twitterIssue)
        {
        }

        public override IList<string> ParentNodes
        {
            get { return new List<string>(){"#BTPH", "#BTP"};}
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
