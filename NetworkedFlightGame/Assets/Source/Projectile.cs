using Source.Client;
using UnityEngine;

namespace Source
{
    public class Projectile : MonoBehaviour
    {
        public int owningEntityId;
        public ClientProjectileHitManager hitManager;

        public void SetVelocity(Vector3 velocity)
        {
            // Todo Implementation
        }
    }
}