using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitPoint : MonoBehaviour {
    
    public int hitPoint = 10;
    public GameObject rootObject;

    void Start() {
        if (rootObject == null)
            rootObject = gameObject;
    }

    public void TakeAHit(int damage) {
        hitPoint -= damage;
        if (hitPoint <= 0) {
            Destroy(rootObject);
        }
    }
}
