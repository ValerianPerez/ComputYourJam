using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletController : BulletController {

    /// <summary>
    /// The particle projectile 
    /// </summary>
    public ParticleSystem projectile;

    // Update is called once per frame
    void Update() {
        if (projectile.IsAlive() == false)
            Destroy(gameObject);
    }

    /// <summary>
    /// Activate the bullet
    /// </summary>
    override public void SetUp(Vector3 direction, Vector3 position, Quaternion rotation)
    {
        transform.rotation = Quaternion.Euler(0, -rotation.eulerAngles.z + 90, 0);
        transform.position += new Vector3(0, 0.5f, 0);
    }
}
