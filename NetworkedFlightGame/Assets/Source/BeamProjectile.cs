using System;
using Source;
using UnityEngine;

public class BeamProjectile : Projectile
{
    [Header("Dependencies")]
    public Rigidbody rb;
    
    [Header("Settings")]
    public float lifetime = 20;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0f, 0f, 500 * Time.deltaTime);
        life();
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitManager.ReportHit(this, collision.gameObject);

        Destroy(gameObject);
    }

    private void life()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    public override void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }
}