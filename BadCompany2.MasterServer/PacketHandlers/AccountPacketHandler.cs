using System.Text;
using BadCompany2.MasterServer.Consts;
using BadCompany2.MasterServer.Services;

namespace BadCompany2.MasterServer.PacketHandlers;

public class AccountPacketHandler(
    GameClient gameClient,
    UserService userService) : BasePacketHandler
{
    public async Task ProcessAsync(string transaction, Packet incomingPacket)
    {
        Transaction = transaction;
        IncomingPacket = incomingPacket;
        
        SendPacket = new Packet(GameClientPacketType.Account, PacketHeader.SendPacketHeader);
        SendPacket.SetVariable(PacketVariable.Transaction, Transaction);
        
        switch (Transaction)
        {
            case AccountTransaction.GetCountryList:
                await GetCountryListAsync();
                return;
            
            case AccountTransaction.GetTermsOfService:
                await GetTermsOfServiceAsync();
                return;
            
            case AccountTransaction.AddAccount:
                await AddAccountAsync();
                return;
            
            case AccountTransaction.Login:
                await LoginAsync();
                return;
            
            case AccountTransaction.GetPersonas:
                await GetPersonasAsync();
                return;
            
            case AccountTransaction.LoginPersona:
                await LoginPersonaAsync();
                return;
            
            case AccountTransaction.AddPersona:
                await AddPersonaAsync();
                return;
            
            case AccountTransaction.DisablePersona:
                await DisablePersonaAsync();
                return;
            
            case AccountTransaction.GetTelemetryToken:
                await GetTelemetryTokenAsync();
                return;
            
            case AccountTransaction.GetEntitlements:
                await GetEntitlementsAsync();
                return;
            
            case AccountTransaction.GrantEntitlement:
                await GrantEntitlementAsync();
                return;
            
            case AccountTransaction.GetLockerUrl:
                await GetLockerUrlAsync();
                return;
            
            case AccountTransaction.SearchOwners:
                await SearchOwnersAsync();
                return;
            
            case AccountTransaction.EntitleGame:
                await EntitleGameAsync();
                return;
            
            case AccountTransaction.EntitleUser:
                await EntitleUserAsync();
                return;
            
            case AccountTransaction.LookupUserInfo:
                await LookupUserInfoAsync();
                return;
        }
    }

    private async Task GetCountryListAsync()
    {
        SendPacket.SetVariable(PacketVariable.CountryList, 0); // Count of countries
    }

    private async Task GetTermsOfServiceAsync()
    {
        SendPacket.SetVariable("version", "20426_20.20426_20");
        
        var termsOfService = await File.ReadAllTextAsync("TermsOfService.txt");
        
        SendPacket.SetVariable("tos", Uri.EscapeDataString(termsOfService));
    }

    private async Task AddAccountAsync()
    {
        var name = IncomingPacket.GetVariable("nuid");

        // Invalid username length
        if (name.Length > 32 || name.Length < 3)
        {
            ushort errorCode = 0;
            string errorType = "";

            if (name.Length > 32)
            {
                errorCode = 3;
                errorType = "TOO_LONG";
            }
            else
            {
                errorCode = 2;
                errorType = "TOO_SHORT";
            }
            
            SendPacket.AddError("21", "\"The required parameters for this call are missing or invalid\"", "displayName", errorCode.ToString(), errorType);
        }
        else if (true /* invalid characters */)
        {
            SendPacket.AddError("21", "\"The required parameters for this call are missing or invalid\"", "displayName", "6", "NOT_ALLOWED");
        }
        else
        {
            var birthdayYear = IncomingPacket.GetVariable("DOBYear");
            var birthdayMonth = IncomingPacket.GetVariable("DOBMonth");
            var birthdayDay = IncomingPacket.GetVariable("DOBDay");

            bool isValid = true;
            
            // Check age limit

            if (isValid)
            {
                var user = new User
                {
                    Username = name,
                    Password = IncomingPacket.GetVariable("password"),
                    Country = IncomingPacket.GetVariable("country"),
                    Birthday = DateTime.Now
                };
                
                await userService.AddAsync(user);
            }
            else
            {
                SendPacket.AddError("21", "\"The required parameters for this call are missing or invalid\"", "dob", "15", "");
            }
        }
    }

    private async Task LoginAsync()
    {
        var username = IncomingPacket.GetVariable("nuid");
        var password = IncomingPacket.GetVariable("password");

        if (String.IsNullOrWhiteSpace(username))
        {
            var decryptedInfo = ParseLoginPacket(IncomingPacket.GetVariable("encryptedInfo"));
            
            username = decryptedInfo.Username;
            password = decryptedInfo.Password;
        }

        if (userService.CheckPassword(username, password))
        {
            var userLoginKey = 
        }
    }

    private (string Username, string Password) ParseLoginPacket(string decryptedInfo)
    {
        string username = "";
        string password = "";
        
        if (String.IsNullOrWhiteSpace(decryptedInfo))
            return ("", ""));

        if (!decryptedInfo.StartsWith(AuthenticationConsts.EncryptedInfoHeader))
            return ("", "");

        decryptedInfo = decryptedInfo.Substring(AuthenticationConsts.EncryptedInfoHeader.Length);

        int pos;

        while ((pos = decryptedInfo.LastIndexOf('-')) >= decryptedInfo.Length - 3 ||
               (pos = decryptedInfo.LastIndexOf('_')) >= decryptedInfo.Length - 3)
        {
            decryptedInfo = decryptedInfo.Remove(pos, 1).Insert(pos, "=");
        }

        decryptedInfo = Encoding.UTF8.GetString(Convert.FromBase64String(decryptedInfo));

        pos = decryptedInfo.IndexOf('\f');

        if (pos != -1)
        {
            username = decryptedInfo.Substring(0, pos);
            password = decryptedInfo.Substring(pos + 1);
        }
        
        return (username, password);
    }

    private async Task GetPersonasAsync()
    {
        var personas = await userService.GetPersonasAsync(gameClient.User.Id);
        
        SendPacket.SetVariable("personas.[]", personas.Count().ToString());

        for (int i = 0; i < personas.Count(); i++)
        {
            SendPacket.SetVariable($"persona.{i}", personas.ElementAt(i).Name);
        }
    }

    private async Task LoginPersonaAsync()
    {
        var persona = await userService.GetPersonaAsync(IncomingPacket.GetVariable("name"));
        
        if (persona == null)
            SendPacket.AddError("101", "\"The user was not found\"");
        else
        {
            // Generate persona L key
            SendPacket.SetVariable("lkey", "");
            SendPacket.SetVariable("profileId", persona.Id);
            SendPacket.SetVariable("userId", persona.User.Id);
            
            // linked_key lkey_id = {persona.id, persona_lkey};

            await userService.LoginAsync(persona);
        }
    }

    private async Task AddPersonaAsync()
    {
        var name = IncomingPacket.GetVariable("name");
        
        
    }

    private async Task DisablePersonaAsync()
    {
        throw new NotImplementedException();
    }

    private async Task GetTelemetryTokenAsync()
    {
        throw new NotImplementedException();
    }

    private async Task GetEntitlementsAsync()
    {
        throw new NotImplementedException();
    }

    private async Task GrantEntitlementAsync()
    {
        throw new NotImplementedException();
    }

    private async Task GetLockerUrlAsync()
    {
        throw new NotImplementedException();
    }

    private async Task SearchOwnersAsync()
    {
        throw new NotImplementedException();
    }

    private async Task EntitleGameAsync()
    {
        throw new NotImplementedException();
    }

    private async Task EntitleUserAsync()
    {
        throw new NotImplementedException();
    }

    private async Task LookupUserInfoAsync()
    {
        throw new NotImplementedException();
    }
}