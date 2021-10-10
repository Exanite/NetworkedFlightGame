using System.Collections;
using System.Collections.Generic;
using Source;
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
    
    void OnTrigger(Collision collision){
        if (collision.gameObject.GetComponent<LocalShip>() != null){
            Debug.Log("collided with a ship");
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 500*Time.deltaTime);
        life();
    }
}
