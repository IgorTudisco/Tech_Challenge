namespace CloudGame.Domain.Entities;

public class Game : Entity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Genre { get; set; }
    public DateTime RegisterAt { get; set; } = DateTime.UtcNow;


    public Game(string name, string description, string imageUrl, decimal price, DateTime releaseDate)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Price = price;
        ReleaseDate = releaseDate;
        Validate();
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentException("Game name cannot be empty or null.");

        if (string.IsNullOrWhiteSpace(Description))
            throw new ArgumentException("Game description cannot be empty or null.");
        
        if(string.IsNullOrWhiteSpace(ImageUrl))
            throw new ArgumentException("Game image URL cannot be empty or null.");

        if(string.IsNullOrWhiteSpace(Genre))
            throw new ArgumentException("Game genre cannot be empty or null.");

        if (Price < 0)
            throw new ArgumentException("Game price cannot be negative or empty.");
        
        if(RegisterAt < DateTime.UtcNow)
            throw new ArgumentException("Register game date in system cannot be in the past.");        
    }

}