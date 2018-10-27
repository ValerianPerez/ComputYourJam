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

        transform.position += velocity * Time.deltaTime;
    }

    /// <summary>
    /// Activate the bullet
    /// </summary>
    public void SetUp(Vector3 direction)
    {
        velocity = direction * Speed;
    }
}
