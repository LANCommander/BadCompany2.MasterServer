namespace BadCompany2.MasterServer.Services;

public class UserService
{
    public async Task<User> AddAsync(User user)
    {
        return user;
    }

    public async Task<IEnumerable<Persona>> GetPersonasAsync(Guid userId)
    {
        return new Persona[] {};
    }

    public async Task<Persona> GetPersonaAsync(string personaName)
    {
        return new Persona {Name = personaName};
    }
}