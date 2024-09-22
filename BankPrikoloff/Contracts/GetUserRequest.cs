namespace BankPrikoloff.Contracts
{
    public class GetUserRequest
    {
        public string ClientId { get; set; } = null!;
        public int RoleId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronomic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int SeriesPasport { get; set; }
        public int NumberPasport { get; set; }
        public string Email { get; set; } = null!;
        public int ChatId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
