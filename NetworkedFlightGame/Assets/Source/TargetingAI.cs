using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Source.Client;
using Source;

public class TargetingAI : MonoBehaviour
{
    public BeamProjectile bulletPrefab;
    public GameObject cannon;
    private Vector3 initialRotation;
    public float maxAngle = 90f; //degrees

    float rate = 0.5f;
    float rechargeTime;
    float dataRecordingRate = 0.1f;
    float dataRechargeTime;

    private List<Vector3> list;
    private Dictionary<int, Ship> playersById;
    private GameObject target;

    // public BulletManager bulletManager;
    void Start(){
        rechargeTime = rate;
        dataRechargeTime = dataRecordingRate;
        initialRotation = transform.forward;

        list = new List<Vector3>();
        for(int i = 0; i < 3; i++){
            list.Add( new Vector3(0f,0f,0f) );
        }
        // playersById = transform.root.GetComponent<ClientPlayerManager>().playersById;
    }

    bool withinAngularThreshold(GameObject target){
        return true;
    }

    Vector3 listV(List<Vector3> list){
        // effectively decasteljau's algorithm
        for(int i = 0; i < list.Count - 1; i++){
            list[i] = list[i] - list[i+1];
        }
        return (list[0] / dataRecordingRate);
    }

    void Fire(){
        // Use best aiming direction
        BeamProjectile bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.localScale *= 0.1f;
        BeamProjectile beamscript = bullet.GetComponent<BeamProjectile>();
        beamscript.spawnerID = 0;//gameObject.name;

        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = transform.forward.normalized * 10;
        // bullet.transform.parent = bulletManager.transform;
    }

    void TryFire(){
        rechargeTime -= Time.deltaTime;
        // Vector3 pointing = Quaternion.Euler(transform.rotation);
        // float angle = Quaternion.Angle(transform.rotation, initialRotation);
        float angle = Vector3.Angle(transform.forward, initialRotation);

        // Debug.Log(angle, maxAngle);
        if(rechargeTime < 0 && angle < maxAngle){
            // Debug.Log("Fire");
            rechargeTime = rate;
            Fire();
        }
    }

    void updateAngles(){
        // Naive, aim directly at target
        Vector3 dp = target.transform.position - transform.position;
        Debug.DrawRay(transform.position, dp, Color.green);
        // transform.rotation = Quaternion.LookRotation(dp.normalized, Vector3.up);
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
        Vector3 dp = (target.transform.position - transform.position);
        Debug.DrawRay(transform.position + dp, v, Color.red);
        Debug.DrawRay(transform.position, dp + v, Color.white);
        //Aim where object is going
        Quaternion aimDir = Quaternion.LookRotation((dp+v).normalized, Vector3.up);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, aimDir, maxAngle);
        transform.rotation = aimDir;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null){
            updateList();
            updateAngles();
            listLook();
            TryFire();
        }
    }
}
