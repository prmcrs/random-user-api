using System;
using System.Collections.Generic;

namespace Random.User.Web.Site.Helpers
{

    public class RootObject
    {
        public int userId { get; set; }
        public string gender { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public DateTime birthDate { get; set; }
        public string uuid { get; set; }
        public string userName { get; set; }
        public int age { get; set; }
        public bool isTheEldest { get; set; }
        public LocationSummary? location { get; set; }
    }

    public class LocationSummary
    {
        public string? street { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postCode { get; set; }
    }
}
