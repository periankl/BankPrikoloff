public class User
{
    public string ClientId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronomic { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int SeriesPasport { get; set; }
    public int NumberPasport { get; set; }
    public string? Email { get; set; }

    public bool AcceptTerms { get; set;}

}

public class BlazorUser
{
    public User User { get; set; }
}