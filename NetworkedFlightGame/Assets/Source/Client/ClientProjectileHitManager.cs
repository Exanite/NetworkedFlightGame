using LiteNetLib;
using Source.Shared;
using UnityEngine;

namespace Source.Client
{
    public class ClientProjectileHitManager : ClientMonoPacketHandler
    {
        public override int HandlerId => (int) Handlers.ProjectileHit;
        
        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            throw new System.NotImplementedException();
        }

        public void ReportHit(Projectile projectile, GameObject objectHit)
        {
            
        }
    }
}