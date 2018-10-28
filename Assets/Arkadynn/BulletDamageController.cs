using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageController : MonoBehaviour {
    
    /// <summary>
    /// the damage of the bullet
    /// </summary>
    public int damage = 10;

    void OnTriggerEnter(Collider other) {
        Debug.Log("");
        if (other.CompareTag("Zombie")) {
            other.GetComponent<ZombieHitPoint>().TakeAHit(damage);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
