using System.ComponentModel.DataAnnotations;

namespace PDP.Web.API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Role { get; set; }

        public UserPassword Password { get; set; }

    }
}