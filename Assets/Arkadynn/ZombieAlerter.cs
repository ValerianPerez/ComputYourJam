using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZombieAlerter : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Zombie")) {
            Debug.Log("Yummy Found");
            other.GetComponent<AgentScript>().NotifyYummy(transform);
        }
    }
}
