using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random.User.Web.Site.Infrastructure
{
    // Options pattern
    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-2.1
    public class ConfigurationOptions
    {
        public string ApiURI { get; set; }
    }
}
