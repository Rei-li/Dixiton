using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHome.Models
{
    public class FishFeederModel
    {
        public int FeedingsCount { get; set; }
        public bool IsFeedingNeeded { get; set; }
        public bool IsLightSwitchingNeeded { get; set; }

    }
}