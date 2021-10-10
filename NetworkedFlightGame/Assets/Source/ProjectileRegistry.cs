using System.Collections.Generic;
using UnityEngine;

namespace Source
{
    [CreateAssetMenu(menuName = "Project/ProjectileRegistry")]
    public class ProjectileRegistry : ScriptableObject
    {
        public List<Beam> projectilePrefabs;

        public bool TryGet(int prefabId, out Beam projectilePrefab)
        {
            projectilePrefab = null;
            
            if (prefabId < 0 ||prefabId >= projectilePrefabs.Count)
            {
                return false;
            }

            projectilePrefab = projectilePrefabs[prefabId];

            return true;
        }
    }
}