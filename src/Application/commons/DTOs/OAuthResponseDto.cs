
namespace Application.commons.DTOs
{
    public class OAuthResponseDto
    {
        public string Iss {get; set;} = null!;
        public string Sub {get; set;} = null!;
        public string Email {get; set;} = null!;
        public bool Email_verified {get; set;}
        public long? Nbf {get; set;}
        public string Name {get; set;} = null!;
        public string Pcture {get; set;} = null!;
        public string Given_name {get; set;} = null!;
        public string Family_name {get; set;} = null!;
        public long? Iat {get; set;}
        public long? Exp {get; set;}
        public string Jti {get; set;} = null!;
    }
}