using BadCompany2.MasterServer.Data;
using Microsoft.EntityFrameworkCore;

namespace BadCompany2.MasterServer.Services;

public class UserService(DatabaseContext context)
{
    public async Task<User> AddAsync(User user)
    {
        await context.AddAsync(user);
        await context.SaveChangesAsync();
        
        return user;
    }

    public async Task<IEnumerable<Persona>> GetPersonasAsync(int userId)
    {
        var user = await context.Users.Include(u => u.Personas).FirstOrDefaultAsync(u => u.Id == userId);
        
        return user?.Personas ?? new List<Persona>();
    }

    public async Task<Persona> GetPersonaAsync(string personaName)
    {
        var persona = await context.Personas.FirstOrDefaultAsync(p => p.Name == personaName);

        return persona;
    }
}