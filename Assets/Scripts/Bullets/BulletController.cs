using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /// <summary>
    /// TTD #Routing
    /// </summary>
    public float TTD = 2;

    /// <summary>
    /// The speed of bullet
    /// </summary>
    public float Speed = 5;

    /// <summary>
    /// The light of projectile
    /// </summary>
    public GameObject Light;

    /// <summary>
    /// The direction of bullet
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// the velocity of beullette
    /// </summary>
    private Vector3 velocity;

    void Update()
    {
        TTD -= Time.deltaTime;

        if (TTD <= 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Activate the bullet
    /// </summary>
    public virtual void SetUp(Vector3 direction, Vector3 position, Quaternion rotation)
    {
        velocity = direction * Speed;
    }

}
