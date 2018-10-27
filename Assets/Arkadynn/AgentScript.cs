using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour {

    private NavMeshAgent agent;

    public Vector3 defaultTarget = new Vector3(1, 0, -0.5f);

    private bool yummyFood = false;
    private int currentAlertLevel = 0;
    private Transform yummyPosition;

    public float attackSpeed = 1;
    private float attackTimer = 0;
    private bool hasAttacked = false;
    

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!yummyFood)
            agent.SetDestination(new Vector3(1, 0, -0.5f));

        if (yummyFood && yummyPosition == null) {
            yummyFood = false;
        }

        if (yummyFood && yummyPosition != null) {
            agent.SetDestination(new Vector3(yummyPosition.position.x, 0, yummyPosition.position.z));

            if (hasAttacked) {
                attackTimer += Time.deltaTime;
                if (attackTimer > attackSpeed) {
                    attackTimer = 0;
                    hasAttacked = false;
                }
                return;
            }

            Ray ray = new Ray(new Vector3(transform.position.x, 4, transform.position.z), transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, .25f)) {

                if (hit.transform.Equals(yummyPosition)) {
                    yummyPosition.GetComponent<ZombieAlerter>().TakeAHit(10);
                    hasAttacked = true;
                }
            }
        }
    }

    public void NotifyYummy (Transform target, int alertLevel) {
        if (yummyFood && currentAlertLevel > alertLevel)
            return;

        yummyFood = true;
        currentAlertLevel = alertLevel;
        yummyPosition = target;
    }
}
