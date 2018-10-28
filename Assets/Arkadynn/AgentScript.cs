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

    private bool hasRecentlyChangedTarget = false;
    public float targetChangingSpeed = 10;
    private float targetChangingTimer = 0;

    private bool hasAttacked = false;
    public float attackSpeed = 1;
    private float attackTimer = 0;
    public int damage = 10;
    public float range = 0.25f;

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
            } else {

                Ray ray = new Ray(transform.position, transform.forward);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, range)) {
                    if (!hit.collider.CompareTag("Zombie") && hit.transform.Equals(yummyPosition)) {
                        hit.collider.gameObject.GetComponent<ZombieAlerter>().TakeAHit(damage);
                    }
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
