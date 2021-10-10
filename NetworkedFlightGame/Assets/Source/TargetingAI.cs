using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingAI : MonoBehaviour
{
    public GameObject target;
    public Beam bulletPrefab;
    public GameObject cannon;
    float rate = 1f;
    float rechargeTime;
    
    float dataRecordingRate = 0.1f;
    float dataRechargeTime;

    private List<Vector3> list;

    // public BulletManager bulletManager;
    void Start(){
        rechargeTime = rate;
        dataRechargeTime = dataRecordingRate;

        list = new List<Vector3>();
        for(int i = 0; i < 3; i++){
            list.Add( target.transform.position);
        }
    }

    bool withinAngularThreshold(GameObject target){
        return true;
    }

    void FireDirect(){
        //Naively point directly at the target
        Vector3 p = transform.position;
        Vector3 dp = (target.transform.position - transform.position).normalized;

        Beam bullet = Instantiate(bulletPrefab, p+dp, transform.rotation);
        bullet.transform.localScale *= 0.1f;
        Beam beamscript = bullet.GetComponent<Beam>();
        beamscript.spawnerID = 0;//gameObject.name;

        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = dp * 10;
        // bullet.transform.parent = bulletManager.transform;
    }

    void TryFire(){
        rechargeTime -= Time.deltaTime;
        if(rechargeTime < 0){
            Debug.Log("Fire");
            rechargeTime = rate;
            FireDirect();
        }
    }

    void updateAngles(){
        // Naive, point directly at target
        Vector3 dp = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dp, Vector3.up);
        Debug.DrawRay(transform.position, dp, Color.green);
    }

    void updateList(){
        if(rechargeTime < 0){
            for(int i = 0; i < list.Count - 1; i++){
                list[i+1] = list[i];
            }
            list[0] = target.transform.position;
            dataRechargeTime = dataRecordingRate;
            Debug.Log("List Update");
        }
    }

    void listLook(List<Vector3> list){
        // effectively decasteljau's algorithm
        for(int i = 0; i < list.Count - 1; i++){
            Debug.Log(list[i]);

            list[i] = list[i] - list[i+1];
        }
        Vector3 v = list[0] / dataRecordingRate;
        Debug.Log(v);
        Vector3 dp = v + target.transform.position - transform.position;
        Debug.DrawRay(transform.position, dp, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        updateAngles();
        updateList();
        listLook(new List<Vector3>(list));
        TryFire();
    }
}
