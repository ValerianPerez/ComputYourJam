using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvasManager : MonoBehaviour
{
    /// <summary>
    /// All "press A or Enter" display
    /// </summary>
    public List<GameObject> AOrSpaceDisplay;

    /// <summary>
    /// All "press A" display
    /// </summary>
    public List<GameObject> ADisplay;

    /// <summary>
    /// All "press A" display
    /// </summary>
    public List<GameObject> PlayerDisplay;

    /// <summary>
    /// The start or enter button
    /// </summary>
    public GameObject StartOrEnterDisplay;

    // Use this for initialization
    void Start()
    {
        foreach (var item in ADisplay)
            item.SetActive(false);

        foreach (var item in PlayerDisplay)
            item.SetActive(false);

        StartOrEnterDisplay.SetActive(false);
    }

    /// <summary>
    /// Detect when Enter is pressed
    /// </summary>
    public void PressSpace()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!PlayerDisplay[i].activeSelf)
            {
                AOrSpaceDisplay[i].SetActive(false);
                ADisplay[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// Display the new player
    /// </summary>
    /// <param name="order">The player order in </param>
    public void NewPlayer(int order)
    {
        if (!StartOrEnterDisplay.activeSelf)
        {
            StartOrEnterDisplay.SetActive(true);
        }

        //1 -> 0
        order--;
        AOrSpaceDisplay[order].SetActive(false);
        ADisplay[order].SetActive(false);
        PlayerDisplay[order].SetActive(true);
    }
}
