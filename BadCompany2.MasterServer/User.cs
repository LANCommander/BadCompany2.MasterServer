namespace BadCompany2.MasterServer;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Country { get; set; }
    public DateTime Birthday { get; set; }
    public string Password { get; set; }
}