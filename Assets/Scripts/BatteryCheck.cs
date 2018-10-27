using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCheck : MonoBehaviour {

    public float range = 5.0f;
    private Light myLight;

	// Use this for initialization
	void Start () {
        myLight = GetComponent<Light>();
        CheckBattery();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void CheckBattery() {
		GameObject[] batteries = GameObject.FindGameObjectsWithTag("Battery");
        myLight.enabled = false;
        foreach (GameObject battery in batteries) {
            Debug.Log(Vector2.Distance(this.transform.position, battery.transform.position));
            if(Vector2.Distance(this.transform.position, battery.transform.position) < range)
            {
                myLight.enabled = true;
                break;
            }
        }
	}

    void OnDrawGizmos()
    {
        //https://answers.unity.com/questions/842981/draw-2d-circle-with-gizmos.html
    }
}
