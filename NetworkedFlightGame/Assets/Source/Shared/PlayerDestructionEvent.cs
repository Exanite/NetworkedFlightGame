using LiteNetLib.Utils;

namespace Source.Shared
{
    public class PlayerDestructionEvent : INetSerializable
    {
        public int Id;
        
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Id);
        }

        public void Deserialize(NetDataReader reader)
        {
            Id = reader.GetInt();
        }
    }
}