namespace Application.commons.DTOs
{
    public class RefreshTokensDto
    {
        public string Token {get; set;} = null!;
        public bool IsRevoked {get; set;}
        public DateTime ExpiryDate {get; set;}
        public DateTime DateCreated {get; set;}
    }
}