namespace CST.Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
