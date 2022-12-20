using NfcPos.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NfcPos.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {

        _context.Users.Add(new Domain.Entities.User()
        {
            Name = "Amadej",
            Surname = "Skornšek",
            Description = "Testni user",
            Balance = 100.5m,
            NfcId = "ABC123"

        });

        _context.Users.Add(new Domain.Entities.User()
        {
            Name = "Blanka",
            Surname = "Kroflič",
            Description = "Testni user",
            Balance = 130.1m,
            NfcId = "ABC111"

        });

        await _context.SaveChangesAsync();

    }
}
