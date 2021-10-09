using LiteNetLib.Utils;

namespace Source.Shared
{
    public class ClientJoinRequest : Event, INetSerializable
    {
        public string PlayerName;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(PlayerName);
        }

        public void Deserialize(NetDataReader reader)
        {
            PlayerName = reader.GetString();
        }
    }
}