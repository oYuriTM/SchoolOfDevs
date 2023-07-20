using SchoolOfDevs.Enums;

namespace SchoolOfDevs.Dto.User
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public TypeUser TypeUser { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Entities.User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            TypeUser = user.TypeUser;
            Token = token;
        }
    }
}
