using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public GameObject turretPrefab; 
    // Start is called before the first frame update
    void Start()
    {
        ring(15f,  1.5f, 5);
        ring(15f, -1.5f, 5);
    }
    void ring(float radius, float z, int n){
        Vector3 p = transform.position;
        for(int i = 0; i < n; i++){
            float a = i * Mathf.PI * 2 / n;
            // float a = i * 360f / n;
            p.x = radius * Mathf.Sin(a);
            p.y = radius * Mathf.Cos(a);
            p.z = z;
            // Quaternion rot = new Quaternion()
            GameObject turret = Instantiate(turretPrefab, p, transform.rotation);
            turret.transform.parent = transform;
        }
    }
}
