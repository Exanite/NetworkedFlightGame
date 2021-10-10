using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>(5);

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 1000; i++){
            make();
        }
    }

    void make(){
        Quaternion q = Random.rotation;
        Vector3 dir = Random.rotation * Vector3.up;
        float r = Random.Range(1000f, 100000f);
        Vector3 p = r * dir;
        // p.x = r;
        int i = Random.Range(0,5);
        GameObject ast = Instantiate(prefabs[i], p, q);
        float s = Mathf.Sqrt(r*2) * Random.Range(5,12);
        ast.transform.localScale += new Vector3(s,s,s);
        // GameObject turret = Instantiate(turretPrefab, p, getRot(front, p));
        ast.transform.parent = transform;
    }
}
