using CleanArchitectureTemplate.Core.Interfaces.Utility.Security;
using CleanArchitectureTemplate.Core.Models.Entities.Users;

namespace CleanArchitectureTemplate.SqlServer;
public static class CleanArchitectureTemplateExtensions
{
    public static void AddInitialData(this CleanArchitectureTemplateContext context, IEncryption encryption)
    {
        context.SeedUsers(encryption);
    }

    private static void SeedUsers(this CleanArchitectureTemplateContext context, IEncryption encryption)
    {
        var id = 1;
        var salt = encryption.GenerateSalt();
        var user = new User
        {
            AccountActivateDate     = DateTimeOffset.Now,
            CreatedById             = id,
            CreatedOn               = DateTimeOffset.Now,
            EmailAddress            = "test@app.com",
            FirstName               = "Admin",
            ImagePath               = "no-image.jpg",
            LastName                = "User",
            IsAccountActivated      = true,
            IsActive                = true,
            PasswordHash            = encryption.GenerateHash("1qazxsw2",salt),
            PasswordSalt            = salt,
            Role                    = UserRole.Admin,
            SecurityStamp           = $"{Guid.NewGuid():N}",
            UniqueId                = Guid.NewGuid(),
            Gender                  = Gender.Male
        };

        if (!context.Users.Any())
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}