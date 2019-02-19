using System;
using System.ComponentModel.DataAnnotations;

namespace Random.User.Web.Site.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Uuid { get; set; }
        public string UserName { get; set; }

        public Location Location { get; set; }
    }

}
