using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterAvailability.Dto;

namespace TwitterAvailability.TwittsProvider
{
    public class MockProvider : ITwittsProvider
    {
        public IList<NakedTwitt> RetrieveTwitts(int count)
        {
            IList<NakedTwitt> result = new List<NakedTwitt>();

            string startingTweetId = "2b8f9ff3-8d4f-4e03-9b45-2a67feafa8d9";//System.Guid.NewGuid().ToString();

            NakedTwitt tefOpen = new NakedTwitt()
                                     {
                                         StringId = startingTweetId + "-001",
                                         CreatedDate = DateTime.Now,
                                         Text = @"#tefop ABC.AR 2012:12:01:22:55 (myID) blablablablablablablablalbalsfsdfsd"
                                     };

            NakedTwitt tefClose = new NakedTwitt()
            {
                StringId = startingTweetId + "-002",
                CreatedDate = DateTime.Now.AddHours(1),
                Text = @"#TEFCL ABC.AR 2012:12:02:22:55 (myID) blablablablablablablablalbalsfsdfsd"
            };


            NakedTwitt btp = new NakedTwitt()
            {
                StringId = startingTweetId + "-003",
                CreatedDate = DateTime.Now.AddHours(2),
                Text = @"#BTP ABC.* 2012:12:02:22:55 33 (theID) bl ab l a b l a  (blablablablablalbalsfsdfsd)"
            };
            NakedTwitt btph = new NakedTwitt()
            {
                StringId = startingTweetId + "-004",
                CreatedDate = DateTime.Now.AddHours(3),
                Text = @"#BTPH ABC.* 2012:12:02:22:55 44 (theID) bl ab l a b l a  (blablablablablalbalsfsdfsd)"
            };


            NakedTwitt btpc = new NakedTwitt()
            {
                StringId = startingTweetId + "-005",
                CreatedDate = DateTime.Now.AddHours(4),
                Text = @"#BTPC ABC.*  (theID) bl ab l a b l a  (blablablablablalbalsfsdfsd)"
            };


            result.Add(tefOpen);
            result.Add(tefClose);
            result.Add(btp);
            result.Add(btph);
            result.Add(btpc);

            return result;
        }
    }
}
