using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    /// <summary>
    /// The move speed
    /// </summary>
    public float Speed = 5;

    /// <summary>
    /// Player Sprite
    /// </summary>
    private GameObject PlayerSprite;

    /// <summary>
    /// look-at player
    /// </summary>
    private GameObject Target;

	// Use this for initialization
	void Start () {
        PlayerSprite = GetComponentsInChildren<GameObject>()[0];
        Target = GetComponentsInChildren<GameObject>()[1];
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, y, 0) * Speed;

        PlayerSprite.transform.position += move;

        Debug.Log(x + " " + y);

        PlayerSprite.transform.LookAt(Target.transform.position);
	}
}
