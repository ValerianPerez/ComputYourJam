using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    /// <summary>
    /// The bullet to fire
    /// </summary>
    public GameObject Bullet;

    /// <summary>
    /// The delay between shot
    /// </summary>
    public float FireRate = 1f;

    /// <summary>
    /// The fire point of gun
    /// </summary>
    public Transform FirePoint;
    
    /// <summary>
    /// Current delay
    /// </summary>
    private float nextFire;


    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Fire with the gun
    /// </summary>
    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;
            BulletController bullet = Instantiate(Bullet, FirePoint.position, transform.rotation).GetComponent<BulletController>();
            bullet.SetUp(FirePoint.up, FirePoint.position, transform.localRotation);
        }
    }
}
