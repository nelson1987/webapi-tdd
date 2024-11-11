namespace Domain;

public class Usuario
{
    public Usuario(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public int Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
}