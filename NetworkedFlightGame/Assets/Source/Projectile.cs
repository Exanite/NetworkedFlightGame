using Source.Client;
using UnityEngine;

namespace Source
{
    public abstract class Projectile : MonoBehaviour
    {
        public int owningEntityId;
        public ClientProjectileHitManager hitManager;

        public abstract void SetVelocity(Vector3 velocity);
    }
}