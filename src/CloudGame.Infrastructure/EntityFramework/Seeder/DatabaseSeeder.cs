using CloudGame.Domain.Entities;
using CloudGame.Domain.Interfaces.Security;
using Microsoft.EntityFrameworkCore;

namespace CloudGame.Infrastructure.EntityFramework.Seeder;

public class DatabaseSeeder()
{
    public static async Task SeedAsync(AppDbContext appDbContext, IPasswordHasher passwordHasher)
    {
        if (await appDbContext.Set<User>().AnyAsync(s => s.IsAdmin))
            return;

        await appDbContext.Set<User>().AddAsync(new User("Admin", "admin@cloudgame.com", passwordHasher.CreateHash("Adm@G4m3"), DateTime.Today.AddYears(-18), true));
        await appDbContext.SaveChangesAsync();
    }
}
