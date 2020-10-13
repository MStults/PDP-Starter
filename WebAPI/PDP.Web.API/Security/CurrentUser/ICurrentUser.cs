namespace PDP.Web.API.Security
{
    public interface ICurrentUser
    {
        int Id { get; }
        string Role { get; }
        string Username { get; }

    }
}