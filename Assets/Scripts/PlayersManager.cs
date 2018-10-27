using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{

    /// <summary>
    /// The player prefab
    /// </summary>
    public GameObject player;

    /// <summary>
    /// The instance for singleton design pattern
    /// </summary>
    private static PlayersManager instance = null;

    /// <summary>
    /// The order of player for instanciation
    /// </summary>
    private List<int> orderOfPlayer;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            Debug.Log("first");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Debug.Log("reject");
            Destroy(this.gameObject);
            return;
        }

        foreach (var item in orderOfPlayer)
        {
            Instantiate(player).GetComponent<PlayerControler>().SetUp(item);
        }
    }

    /// <summary>
    /// Define the order of player
    /// </summary>
    public void SetOrderOfPlayer(List<int> order)
    {
        orderOfPlayer = order;
    }
}
