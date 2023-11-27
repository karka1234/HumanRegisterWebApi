using static HumanRegisterWebApi.Enums.Enums;

namespace HumanRegisterWebApi.Requests
{
    public class SignupNewAccountRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
