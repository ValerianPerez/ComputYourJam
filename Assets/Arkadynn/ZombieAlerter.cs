using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZombieAlerter : MonoBehaviour {

    public int alertLevel = 0;

    public int hitPoints = 20;
    public GameObject rootObject;

    void Start() {
        if (rootObject == null) {
            rootObject = gameObject;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Zombie")) {
            Debug.Log("Yummy Found");
            other.GetComponent<AgentScript>().NotifyYummy(transform, alertLevel);
        }
    }

    public void TakeAHit (int damage) {
        hitPoints -= damage;

        if (hitPoints <= 0) {
            rootObject.SetActive(false);
        }
    }
}
