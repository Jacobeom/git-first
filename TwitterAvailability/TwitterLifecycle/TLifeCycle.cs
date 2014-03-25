using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterAvailability.Dto;
using TwitterAvailability.TwitterLifecycle;

namespace TwitterAvailability.TwitterLifecycle
{
     public class TLifeCycle : TwitterLifecycle
    {
         public TLifeCycle(TwitterIssue twitterIssue) : base(twitterIssue)
         {
         }

         public override IList<string> ParentNodes
         {
             get { throw new NotImplementedException(); }
         }

         public override bool IsRoot
         {
             get { throw new NotImplementedException(); }
         }

         public override bool IsLeaf
         {
             get { throw new NotImplementedException(); }
         }
    }
}
