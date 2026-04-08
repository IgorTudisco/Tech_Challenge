namespace CloudGame.Domain.Entities;

public sealed class User : Entity<int>
{
    public User(string name, string email, string password, DateTime birthDate, bool isAdmin)
    {
        Name = name;
        Email = email;
        SetPassword(password);
        BirthDate = birthDate;
        Active = true;
        IsAdmin = isAdmin;
    }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public DateTime BirthDate { get; private set; }

    public bool Active { get; private set; }

    public DateTime? UpdateAt { get; private set; }

    public bool IsAdmin { get; private set; }

    public void SetActive(bool active)
    {
        Active = active;
    }

    public void SetPassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("A senha é obrigatoria.");

        Password = passwordHash;
    }

    public void SetUpdateAt()
    {
        UpdateAt = DateTime.UtcNow;
    }

    public static User CreatingUser()
    {
        var user = new User("admin", "admin@cloudgame.com", "admingame", new DateTime(2026, 3, 31), true)
        {
            CreatedAt = new DateTime(2026, 03, 31)
        };
        return user;
    }

    private User() { }
}