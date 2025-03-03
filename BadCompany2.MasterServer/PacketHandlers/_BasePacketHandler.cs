namespace BadCompany2.MasterServer.PacketHandlers;

public abstract class BasePacketHandler
{
    protected Packet SendPacket;
    protected string Transaction;
    protected Packet IncomingPacket;
}