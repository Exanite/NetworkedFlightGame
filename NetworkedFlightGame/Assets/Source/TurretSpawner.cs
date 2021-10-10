using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public GameObject turretPrefab; 
    // Start is called before the first frame update
    void Start()
    {
        concentric();
    }

    void concentric(){
        both(3.5f, 2.7f,   5,  1);
        both(15f,    1f,   5,  1);
        both(38f,    1f,   5,  1);
        both(48f,  1.5f, 6*5,  1);
        ring(50.5f,  0f, 6*5,  0);
    }

    void both(float r, float z, int n, int f){
        ring(r,  z, n,  f);
        ring(r, -z, n, -f);
    }

    Quaternion lookAt(Vector3 t){
        Vector3 dp = t - transform.position;
        // Debug.DrawRay(transform.position, dp, Color.green);
        Quaternion aimDir = Quaternion.LookRotation(dp.normalized, Vector3.up);
        return aimDir;
    }

    Quaternion getRot(int front, Vector3 p){
        return front != 0 
            ? Quaternion.LookRotation(front * transform.forward, Vector3.up)
            : lookAt(p);
    }

    void ring(float radius, float z, int n, int front){
        Vector3 p = transform.position;
        for(int i = 0; i < n; i++){
            float a = i * Mathf.PI * 2 / n; //n spokes
            // float a = i * 360f / n;
            p.x = radius * Mathf.Sin(a);
            p.y = radius * Mathf.Cos(a);
            p.z = z;
            GameObject turret = Instantiate(turretPrefab, p, getRot(front, p));
            turret.transform.parent = transform;
        }
    }
}
