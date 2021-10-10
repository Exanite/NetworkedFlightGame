using System.Collections.Generic;
using System.Linq;
using Source;
using Source.Client;
using UnityEngine;

public class TargetingAI : MonoBehaviour
{
    public BeamProjectile bulletPrefab;
    public GameObject cannon;
    private Vector3 initialRotation;
    public float maxAngle = 90f; //degrees

    private readonly float rate = 2f;
    private float rechargeTime;
    private readonly float dataRecordingRate = 0.1f;
    private float dataRechargeTime;

    private List<Vector3> list;
    private Dictionary<int, Ship> playersById;
    private Ship target;

    // public BulletManager bulletManager;
    private void Start()
    {
        rechargeTime = rate;
        dataRechargeTime = dataRecordingRate;
        initialRotation = transform.forward;

        list = new List<Vector3>();
        for (var i = 0; i < 3; i++)
        {
            list.Add(new Vector3(0f, 0f, 0f));
        }
        // playersById = transform.root.GetComponent<ClientPlayerManager>().playersById;

        playersById = FindObjectOfType<ClientPlayerManager>().playersById;
    }

    private bool withinAngularThreshold(GameObject target)
    {
        return true;
    }

    private Vector3 listV(List<Vector3> list)
    {
        // effectively decasteljau's algorithm
        for (var i = 0; i < list.Count - 1; i++)
        {
            list[i] = list[i] - list[i + 1];
        }

        return list[0] / dataRecordingRate;
    }

    private void Fire()
    {
        // Use best aiming direction
        var p = transform.position + transform.forward * 10;
        var bullet = Instantiate(bulletPrefab, p, transform.rotation);
        bullet.transform.localScale *= 2f;
        var beamscript = bullet.GetComponent<BeamProjectile>();

        var bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = transform.forward.normalized * 300;
        // bullet.transform.parent = bulletManager.transform;
    }

    private void TryFire()
    {
        rechargeTime -= Time.deltaTime;
        // Vector3 pointing = Quaternion.Euler(transform.rotation);
        // float angle = Quaternion.Angle(transform.rotation, initialRotation);
        var angle = Vector3.Angle(transform.forward, initialRotation);

        // Debug.Log(angle, maxAngle);
        if (rechargeTime < 0 && angle < maxAngle)
        {
            // Debug.Log("Fire");
            rechargeTime = rate;
            Fire();
        }
    }

    private void updateAngles()
    {
        // Naive, aim directly at target
        var dp = target.transform.position - transform.position;
        Debug.DrawRay(transform.position, dp, Color.green);
        // transform.rotation = Quaternion.LookRotation(dp.normalized, Vector3.up);
    }

    private void updateList()
    {
        dataRechargeTime -= Time.deltaTime;
        if (dataRechargeTime < 0)
        {
            for (var i = 0; i < list.Count - 1; i++)
            {
                list[i + 1] = list[i];
            }

            list[0] = target.transform.position;
            dataRechargeTime = dataRecordingRate;
        }
    }

    private void listLook()
    {
        var v = listV(new List<Vector3>(list));
        var dp = target.transform.position - transform.position;
        Debug.DrawRay(transform.position + dp, v, Color.red);
        Debug.DrawRay(transform.position, dp + v, Color.white);
        //Aim where object is going
        var aimDir = Quaternion.LookRotation((dp + v).normalized, Vector3.up);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, aimDir, maxAngle);
        transform.rotation = aimDir;
    }

    // Update is called once per frame
    private void Update()
    {
        var targetRange = 500f;
        var closestDistance = Mathf.Infinity;
        var closestShip = playersById.Values.First();

        foreach (var ship in playersById.Values)
        {
            var distance = Vector3.Distance(transform.position, ship.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestShip = ship;
            }
        }

        target = closestShip;

        if (closestDistance > targetRange)
        {
            target = null;
        }

        if (target != null)
        {
            updateList();
            updateAngles();
            listLook();
            TryFire();
        }
    }
}