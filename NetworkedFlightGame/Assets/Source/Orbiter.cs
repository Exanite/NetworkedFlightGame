using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public float radius = 8f;
    // Update is called once per frame
    void Update()
    {
        float x = radius * Mathf.Cos(Time.time);
        float z = radius * Mathf.Sin(Time.time);
        transform.position = new Vector3(x,transform.position.y,z);        
    }
}
