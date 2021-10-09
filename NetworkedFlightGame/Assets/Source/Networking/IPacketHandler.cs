using LiteNetLib;

namespace Networking
{
    public interface IPacketHandler
    {
        int Id { get; }
        
        void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod);
    }
}