using LiteNetLib.Utils;
using Networking;
using UnityEngine;

namespace Source.Shared
{
    public class PlayerStatePacket : INetSerializable
    {
        public int NetworkId;
        public float lastReceiveTime; // Not implemented
        
        public Vector3 Position;
        public Quaternion Rotation;

        public void Set(int networkId, Vector3 position, Quaternion rotation)
        {
            NetworkId = networkId;
            Position = position;
            Rotation = rotation;
        }
        
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(NetworkId);
            
            writer.Put(Position);
            writer.Put(Rotation);
        }

        public void Deserialize(NetDataReader reader)
        {
            NetworkId = reader.GetInt();
            
            Position = reader.GetVector3();
            Rotation = reader.GetQuaternion();
        }
    }
}