using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    Vector3 initial_position;
    float lifetime;
    public int spawnerID;

    // Start is called before the first frame update
    void Start()
    {
        initial_position = transform.position;
        lifetime = 20;
        transform.parent.GetComponent<BulletManager>().newChild(gameObject);
    }

    void life(){
        lifetime -= Time.deltaTime;
        if (lifetime < 0){
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.GetComponent<Ship>() != null){
            Debug.Log("collided with a ship");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        life();
    }
}
