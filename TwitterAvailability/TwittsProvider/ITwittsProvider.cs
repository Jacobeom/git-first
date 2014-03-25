using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterAvailability.Dto;

namespace TwitterAvailability.TwittsProvider
{
    public interface ITwittsProvider
    {
        IList<NakedTwitt> RetrieveTwitts(int count);
    }
}
