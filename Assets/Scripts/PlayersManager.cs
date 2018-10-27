using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour {

    private List<PlayerControler> players;

	// Use this for initialization
	void Start () {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        players = new List<PlayerControler>();

        for (int i = 0; i < p.Length; i++)
        {
            players.Add(p[i].GetComponent<PlayerControler>().SetUp(i+1));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
