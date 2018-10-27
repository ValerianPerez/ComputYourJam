using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZombieAlerter : MonoBehaviour {

    public int alertLevel = 0;

    public int hitPoint = 20;

    public GameObject rootObject;

    void Start() {
        if (rootObject == null)
            rootObject = gameObject;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Zombie")) {
            Debug.Log("Yummy Found");
            other.GetComponent<AgentScript>().NotifyYummy(transform, alertLevel);
        }
    }

    public void TakeAHit(int damage) {
        hitPoint -= damage;
        if (hitPoint <= 0) {
            Debug.Log("Agent " + rootObject.name + " is down !!!! : ");
            rootObject.SetActive(false);
        }
    }
}
