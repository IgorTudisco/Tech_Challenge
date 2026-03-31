namespace CloudGame.Domain.Entities;

public sealed class User : Entity<int>
{
    public User(string name, string email, string password, DateTime birthDate)
    {
        Name = name;
        Email = email;
        SetPassword(password);
        BirthDate = birthDate;
        Active = true;
    }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public DateTime BirthDate { get; private set; }

    public bool Active { get; private set; }

    public DateTime? UpdateAt { get; private set; }

    public void SetActive(bool active)
    {
        Active = active;
    }

    public void SetPassword(string password)
    {
        if (password?.Length < 8)
            throw new ArgumentException("A senha precisa ter no minino 8 caracteres.");

        Password = password!;
    }

    public void SetUpdateAt()
    {
        UpdateAt = DateTime.UtcNow;
    }

}