using LiteNetLib.Utils;
using Networking;
using UnityEngine;

public class EntityCreationEvent : INetSerializable
{
    public int prefabId;
    
    public Vector3 position;
    public Quaternion rotation;

    public Vector3 velocity;
    
    public void Serialize(NetDataWriter writer)
    {
        writer.Put(prefabId);
        
        writer.Put(position);
        writer.Put(rotation);

        writer.Put(velocity);
    }

    public void Deserialize(NetDataReader reader)
    {
        prefabId = reader.GetInt();

        position = reader.GetVector3();
        rotation = reader.GetQuaternion();
        
        velocity = reader.GetVector3();
    }
}