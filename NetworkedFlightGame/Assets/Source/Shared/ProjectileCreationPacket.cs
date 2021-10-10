using LiteNetLib.Utils;
using Networking;
using UnityEngine;

namespace Source.Shared
{
    public class ProjectileCreationPacket : INetSerializable
    {
        public int PrefabId;
        public int OwningEntityId;

        public Vector3 Position;
        public Quaternion Rotation;

        public Vector3 Velocity;

        public void Set(int prefabId, int owningEntityId, Vector3 position, Quaternion rotation, Vector3 velocity)
        {
            PrefabId = prefabId;
            OwningEntityId = owningEntityId;
            Position = position;
            Rotation = rotation;
            Velocity = velocity;
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(PrefabId);
            writer.Put(OwningEntityId);

            writer.Put(Position);
            writer.Put(Rotation);

            writer.Put(Velocity);
        }

        public void Deserialize(NetDataReader reader)
        {
            PrefabId = reader.GetInt();
            OwningEntityId = reader.GetInt();

            Position = reader.GetVector3();
            Rotation = reader.GetQuaternion();

            Velocity = reader.GetVector3();
        }
    }
}