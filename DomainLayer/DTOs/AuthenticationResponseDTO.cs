namespace DomainLayer.DTOs
{
    public class AuthenticationResponseDTO
    {
        public string token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
