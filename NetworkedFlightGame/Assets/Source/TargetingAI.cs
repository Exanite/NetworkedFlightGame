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

    Vector3 listV(List<Vector3> list){
        // effectively decasteljau's algorithm
        for(int i = 0; i < list.Count - 1; i++){
            list[i] = list[i] - list[i+1];
        }
        return list[0] / dataRecordingRate;
    }


    void FireDirect(){
        //Naively point directly at the target
        Vector3 p = transform.position;
        Vector3 dp = (target.transform.position - transform.position).normalized;

        // account for velocity
        // Vector3 v = listV(new List<Vector3>(list));
        // dp = (dp+v).normalized;

        Beam bullet = Instantiate(bulletPrefab, p, transform.rotation);
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
        Debug.DrawRay(transform.position, dp, Color.green);
        transform.rotation = Quaternion.LookRotation(dp.normalized, Vector3.up);
    }

    void updateList(){
        dataRechargeTime -= Time.deltaTime;
        if(dataRechargeTime < 0){
            for(int i = 0; i < list.Count - 1; i++){
                list[i+1] = list[i];
            }
            list[0] = target.transform.position;
            dataRechargeTime = dataRecordingRate;
        }
    }

    void listLook(){
        Vector3 v = listV(new List<Vector3>(list));
        Vector3 dp = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dp+v, Vector3.up);
        // Debug.DrawRay(dp, v, Color.red);
        // Debug.DrawRay(transform.position, dp + v, Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        updateAngles();
        // updateList();
        listLook();
        TryFire();
    }
}
