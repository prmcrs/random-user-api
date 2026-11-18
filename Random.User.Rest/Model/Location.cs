using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Random.User.Rest.Model
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string State { get; set; } = "";
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string PostCode { get; set; } = "";

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }

}
