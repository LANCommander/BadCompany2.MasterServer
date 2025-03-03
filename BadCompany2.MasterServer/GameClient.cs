using Microsoft.EntityFrameworkCore;

namespace BadCompany2.MasterServer;

public class GameClient
{
    private ConnectionType _connectionType;
    private readonly DbContext _databaseContext;
    
    public User User;
    private Persona _persona;

    private string _userLKey;
    private string _personaLKey;

    private Stats _stats;

    private int sockTID;
    private int queuePosition;
    private int originalQueueLength;

    private bool _isLocal;
    private string emulatorId;
    private bool alreadyLoggedIn;

    private List<string> playerList;

    private void FilterGames(Packet packet, List<int> filtered)
    {
        
    }

    public bool AcctPacket(List<Packet> queue, Packet packet, string txn, string ip)
    {
        
    }

    public bool AssoPacket(List<Packet> queue, Packet packet, string txn)
    {
        
    }
    
    public bool XmsgPacket(List<Packet> queue, Packet packet, string txn)
    {
        
    }
    
    public bool PresPacket(List<Packet> queue, Packet packet, string txn)
    {
        
    }
    
    public bool RecpPacket(List<Packet> queue, Packet packet, string txn)
    {
        
    }
    
    public bool PnowPacket(List<Packet> queue, Packet packet, string txn)
    {
        
    }
    
    public int RankPacket(List<Packet> queue, Packet packet, string txn)
    {
        
    }

    public int ProcessTheater(List<Packet> queue, Packet packet, char type, string ip)
    {
        
    }
}