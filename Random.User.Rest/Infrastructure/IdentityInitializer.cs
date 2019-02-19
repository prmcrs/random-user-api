using Newtonsoft.Json;
using Random.User.Rest.Infrastructure;
using Random.User.Rest.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Random.User.Rest.Infrastructure
{
    public class IdentityInitializer
    {
        public readonly ConfigurationOptions configuration;
        private readonly DataContext context;

        public IdentityInitializer(DataContext context, ConfigurationOptions configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task Seed()
        {
            Uri generatorUri = new Uri(configuration.GeneratorSiteURI);
            var client = new WebClient();
            string data = await client.DownloadStringTaskAsync(generatorUri.AbsoluteUri);
            var rs = JsonConvert.DeserializeObject<RootObject>(data);

            if (rs.Results.Count != 0)
            {
                foreach (var i in rs.Results)
                {
                    Model.User user = new Model.User
                    {
                        Gender = i.Gender,
                        Name = i.Name.Title + " " + i.Name.First + " " + i.Name.Last,
                        Email = i.Email,
                        BirthDate = i.Dob.Date,
                        Uuid = i.Login.Uuid,
                        UserName = i.Login.Username,
                        Age = i.Dob.Age,
                    };

                    context.Users.Add(user);

                    Model.Location location = new Model.Location
                    {
                        UserId = user.UserId,
                        State = i.Location.State,
                        Street = i.Location.Street,
                        City = i.Location.City,
                        PostCode = i.Location.Postcode
                    };

                    context.Locations.Add(location);

                    await context.SaveChangesAsync();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}

