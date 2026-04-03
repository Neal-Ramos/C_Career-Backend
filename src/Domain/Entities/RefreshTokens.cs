namespace Domain.Entities
{
    public class RefreshTokens
    {
        public int Id {get; private set;}
        public string Token {get; set;} = null!;
        public bool IsRevoked {get; set;}
        public DateTime ExpiryDate {get; set;}
        public DateTime DateCreated {get; set;}
    }
}