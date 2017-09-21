using Microsoft.IdentityModel.Tokens;

namespace Readme.Web.Api
{
    public class TokenAuthOption
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public string Secret_key { get; set; }
    }
}
