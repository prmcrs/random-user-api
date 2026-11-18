using System;
using System.ComponentModel.DataAnnotations;

namespace Random.User.Rest.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required, StringLength(20)]
        public string Gender { get; set; } = "";
        [Required, StringLength(150)]
        public string Name { get; set; } = "";
        [Required, EmailAddress, StringLength(254)]
        public string Email { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public string Uuid { get; set; } = "";
        [Required, StringLength(100)]
        public string UserName { get; set; } = "";
        public int Age { get; set; }
        public bool IsTheEldest{ get; set; }
        public Location? Location { get; set; }
    }

}
