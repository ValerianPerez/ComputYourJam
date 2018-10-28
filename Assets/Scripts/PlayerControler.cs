using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerControler : MonoBehaviour
{
    /// <summary>
    /// The camera prefab
    /// </summary>
    public GameObject Camera;

    /// <summary>
    /// moving part of the player
    /// </summary>
    public GameObject playerSprite;

    /// <summary>
    /// private reference to the camera following the player
    /// </summary>
    private Camera cam;
    
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

    /// <summary>
    /// The animator of character sprite
    /// </summary>
    [SerializeField]
    private Animator characterAnimator;

    /// <summary>
    /// The renderer of character sprite
    /// </summary>
    [SerializeField]
    private SpriteRenderer characterRenderer;

    /// <summary>
    /// The gun renderer
    /// </summary>
    [SerializeField]
    private SpriteRenderer gunRenderer;

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

        //Define animation
        Vector2 movment = new Vector2(x, y);
        float angle_dir = Vector2.SignedAngle(movment, Vector2.up);

        if (movment == Vector2.zero)
        {
            characterAnimator.SetBool("isRunning", false);
            characterRenderer.flipX = false;
        }
        else
        {
            characterAnimator.SetBool("isRunning", true);

            if (0 < angle_dir && angle_dir < 180)
            {
                characterRenderer.flipX = false;
            }
            else
            {
                characterRenderer.flipX = true;
            }
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
        Gun.transform.localRotation = Quaternion.Euler(0, 0, angle);

        float gunAngle = Gun.transform.localRotation.eulerAngles.z;


        //Flip the gun on Y Axis
        if (0 < gunAngle && gunAngle <= 90 || 270 < gunAngle && gunAngle <= 360)
        {
            gunRenderer.flipY = false;
        }
        else
        {
            gunRenderer.flipY = true;
        }

        //Gun is hide by the character head
        if (0 < gunAngle && gunAngle < 180)
        {
            gunRenderer.sortingOrder = 2;
        }
        else
        {
            gunRenderer.sortingOrder = 4;
        }

        if (isKeyboard)
        {
            if (Input.GetMouseButton(0))
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
    public PlayerControler SetUp(int padNumber, int nbPlayer, int playerNumber)
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
        
        GameObject cam_go = Instantiate(Camera);
        DontDestroyOnLoad(cam_go);
        cam = cam_go.GetComponentInChildren<Camera>();
        CinemachineVirtualCamera cvc = cam_go.GetComponentInChildren<CinemachineVirtualCamera>();
        cvc.Follow = playerSprite.transform;
        cvc.LookAt = playerSprite.transform;

        switch(playerNumber) {
            case 1:
                cam.cullingMask += 1 << 9;
                cam.gameObject.layer = 9;
                cvc.gameObject.layer = 9;
                break;
            case 2:
                cam.cullingMask += 1 << 10;
                cvc.gameObject.layer = 10;
                break;
            case 3:
                cam.cullingMask += 1 << 11;
                cvc.gameObject.layer = 11;
                break;
            case 4:
                cam.cullingMask += 1 << 12;
                cvc.gameObject.layer = 12;
                break;
        }

        switch (nbPlayer) {
            case 1:
                cam.rect = new Rect(0, 0, 1, 1);
                break;
            case 2:
                if (playerNumber == 1) {
                    cam.rect = new Rect(0, 0.5f, 1, .5f);
                } else {
                    cam.rect = new Rect(0, 0, 1, .5f);
                }
                break;
            case 3:
                if (playerNumber == 1) {
                    cam.rect = new Rect(0, 0.5f, .5f, .5f);
                } else if (playerNumber == 2) {
                    cam.rect = new Rect(0.5f, 0.5f, .5f, .5f);
                } else {
                    cam.rect = new Rect(0.25f, 0, .5f, .5f);
                }
                break;
            case 4:
                if (playerNumber == 1) {
                    cam.rect = new Rect(0, 0.5f, .5f, .5f);
                } else if (playerNumber == 2) {
                    cam.rect = new Rect(0.5f, 0.5f, .5f, .5f);
                } else if (playerNumber == 3) {
                    cam.rect = new Rect(0, 0, .5f, .5f);
                } else {
                    cam.rect = new Rect(0.5f, 0, .5f, .5f);
                }
                break;
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
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
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
