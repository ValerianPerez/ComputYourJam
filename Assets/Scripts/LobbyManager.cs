using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    /// <summary>
    /// The maximum of authorized players
    /// </summary>
    public int maxNumberOfPlayer = 4;

    /// <summary>
    /// The canvas of lobby
    /// </summary>
    public LobbyCanvasManager canvas;

    /// <summary>
    /// Taboo list of already pressed A buttons
    /// </summary>
    private List<int> AlreadyPressedButtons;

    /// <summary>
    /// The position of next free slot
    /// </summary>
    private int nextFreeSlot;

    /// <summary>
    /// List of pad link to player
    /// </summary>
    private List<int> OrderOfPad;

    // Use this for initialization
    void Start()
    {
        AlreadyPressedButtons = new List<int>();
        nextFreeSlot = 1;
        OrderOfPad = new List<int>(4);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!AlreadyConnected(0))
            {
                AlreadyPressedButtons.Add(0);
                TakeNextFreeSlot(0);
            }
        }
        else if (Input.GetButtonDown("A_Button_1"))
        {
            if (!AlreadyConnected(1))
            {
                AlreadyPressedButtons.Add(1);
                TakeNextFreeSlot(1);
            }
        }
        else if (Input.GetButtonDown("A_Button_2"))
        {
            if (!AlreadyConnected(2))
            {
                AlreadyPressedButtons.Add(2);
                TakeNextFreeSlot(2);
            }
        }
        else if (Input.GetButtonDown("A_Button_3"))
        {
            if (!AlreadyConnected(3))
            {
                AlreadyPressedButtons.Add(3);
                TakeNextFreeSlot(3);
            }
        }
        else if (Input.GetButtonDown("A_Button_4"))
        {
            if (!AlreadyConnected(4))
            {
                AlreadyPressedButtons.Add(4);
                TakeNextFreeSlot(4);
            }
        }
        else if (Input.GetButtonDown("Start_Button_1")
                || Input.GetButtonDown("Start_Button_2")
                || Input.GetButtonDown("Start_Button_3")
                || Input.GetButtonDown("Start_Button_4")
                || Input.GetKeyDown(KeyCode.Return)
                || nextFreeSlot > 1)
        {

        }


    }

    /// <summary>
    /// Check is the pad is connected
    /// </summary>
    /// <param name="padNumber">The pad number</param>
    /// <returns>True if the pad  is already connected</returns>
    private bool AlreadyConnected(int padNumber)
    {
        return AlreadyPressedButtons.Contains(padNumber);
    }

    /// <summary>
    /// Register the pad to a slot, 0 for keyboard
    /// <param name="padNumber">The pad number</param>
    /// </summary>
    private void TakeNextFreeSlot(int numPad)
    {
        if (nextFreeSlot > maxNumberOfPlayer)
            return;

        OrderOfPad.Add(numPad);

        if (numPad == 0)
        {
            canvas.PressSpace();
        }

        canvas.NewPlayer(nextFreeSlot);
        
        nextFreeSlot++;
    }
}
