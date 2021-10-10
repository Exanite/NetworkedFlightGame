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

            if (prefabId < 0 || prefabId >= projectilePrefabs.Count)
            {
                return false;
            }

            projectilePrefab = projectilePrefabs[prefabId];

            return true;
        }

        /// <summary>
        /// Use if you want to reference projectiles by prefab on the Ship/turrets and then get the id from here
        /// </summary>
        public int GetId(Beam projectilePrefab)
        {
            return projectilePrefabs.IndexOf(projectilePrefab);
        }
    }
}