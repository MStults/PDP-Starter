namespace PDP.Web.API.Models
{
    public class UserPassword
    {
        public int Id { get; set; }
     
        public byte[] Hash { get; set; }

        public byte[] Salt { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}