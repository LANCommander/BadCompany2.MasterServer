namespace BadCompany2.MasterServer.Consts;

public class PacketHeader
{
    public const uint SendPacketHeader = 0x80000000;
    public const uint TheaterPacketHeader = 0x00000000;
    public const uint EGAMQueueHeader = 0x71756575;
}