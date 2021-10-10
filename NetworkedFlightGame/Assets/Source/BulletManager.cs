using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BulletFactory
public class BulletManager : MonoBehaviour
{
    public List<GameObject> bulletPrefabs;
    
    //send create / destroy events
    //
    public void newChild(GameObject go){
        Debug.Log(go.name);
    }

    public void CreateProjectile(int prefabId, Vector3 position, Quaternion rotation)
    {
        
        // Send network message
    }
}