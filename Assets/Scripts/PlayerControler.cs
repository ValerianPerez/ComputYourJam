using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    /// <summary>
    /// The move speed
    /// </summary>
    public float Speed = 5;

    /// <summary>
    /// The sensitivity for gamepad
    /// </summary>
    public float Sensitivity = 5;

    /// <summary>
    /// Define if the controller is with keyboard or pad
    /// </summary>
    public bool isKeyboard;

    /// <summary>
    /// The current gun
    /// </summary>
    public Transform Gun;

    /// <summary>
    /// Player Sprite
    /// </summary>
    private Transform PlayerSprite;

    /// <summary>
    /// The previous rotation
    /// </summary>
    private Vector3 OldRotation;

    /// <summary>
    /// The string for left stick X axis 
    /// </summary>
    private string leftStickX = "Left_stick_X_";

    /// <summary>
    /// The string for left stick Y axis 
    /// </summary>
    private string leftStickY = "Left_stick_Y_";

    /// <summary>
    /// The string for right stick X axis 
    /// </summary>
    private string rightStickX = "Right_stick_X_";

    /// <summary>
    /// The string for right stick Y axis 
    /// </summary>
    private string rightStickY = "Right_stick_Y_";

    /// <summary>
    /// The string for A button 
    /// </summary>
    private string aButton = "A_Button_";

    /// <summary>
    /// The string for B button
    /// </summary>
    private string bButton = "B_Button_";

    /// <summary>
    /// Define when the fire is triggered
    /// </summary>
    private bool isTriggerFire;

    /// <summary>
    /// Singleton
    /// </summary>
    private PlayerControler instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x;
        float y;

        if (isKeyboard)
        {
            //Get movment input
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
        }
        else
        {
            x = Input.GetAxis(leftStickX);
            y = Input.GetAxis(leftStickY);
        }

        //Assign movment
        Vector3 move = new Vector3(x, y, 0) * Speed;
        PlayerSprite.localPosition += move * Time.deltaTime;

        //Get position of target
        if (isKeyboard)
        {
            move = GetKeyboardTargetPostion();
        }
        else
        {
            move = GetPadTargetPostion();
        }

        //Compute rotation
        Vector3 dir = move - PlayerSprite.localPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Assign rotation
        Gun.position = PlayerSprite.position;
        Gun.transform.localRotation = Quaternion.Euler(0, 0, angle - 90);

        if (isKeyboard)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isTriggerFire = true;
            }
        }
        else
        {
            if (Input.GetButtonDown(aButton))
            {
                Debug.Log(aButton);
            }
        }

        if (isTriggerFire)
        {
            Gun.gameObject.GetComponent<GunController>().Fire();
            isTriggerFire = false;
        }
    }

    /// <summary>
    /// The class for set-up the pad
    /// </summary>
    /// <param name="padNumber">The pad number</param>
    public PlayerControler SetUp(int padNumber, int nbPlayer)
    {
        if (padNumber < 0 || padNumber > 4)
        {
            throw new Exception("PadNumber must be between 0 and 4");
        }

        //The root component must not be keep
        PlayerSprite = GetComponentsInChildren<Transform>()[1];

        if (padNumber == 0)
        {
            isKeyboard = true;
        }
        else
        {
            //Define control strings
            leftStickX += padNumber;
            leftStickY += padNumber;
            rightStickX += padNumber;
            rightStickY += padNumber;
            aButton += padNumber;
            bButton += padNumber;
        }

        return this;
    }

    /// <summary>
    /// Compute the position of target with keyboard
    /// </summary>
    /// <returns>Returns the vector position</returns>
    private Vector3 GetKeyboardTargetPostion()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = new Vector3(mousePosition.x, mousePosition.z, 0);
        return mousePosition;
    }

    /// <summary>
    /// Compute the position of target with pad
    /// </summary>
    /// <returns>Returns the vector position</returns>
    private Vector3 GetPadTargetPostion()
    {
        float x = Input.GetAxis(rightStickX);
        float y = Input.GetAxis(rightStickY);

        Vector3 rot = new Vector3(x, y, 0) * Sensitivity;

        if (rot.sqrMagnitude == 0)
        {
            rot = OldRotation;
        }
        else
        {
            OldRotation = rot;
            isTriggerFire = true;
        }

        return PlayerSprite.localPosition + rot;
    }
}
