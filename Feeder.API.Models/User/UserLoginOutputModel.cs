namespace Feeder.API.Models.User
{
    public class UserLoginOutputModel
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
