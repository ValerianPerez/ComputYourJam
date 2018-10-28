using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{

    /// <summary>
    /// The player prefab
    /// </summary>
    public GameObject player;

    /// <summary>
    /// The Cameras prefab
    /// </summary>
    private GameObject Cameras;

    /// <summary>
    /// The instance for singleton design pattern
    /// </summary>
    private static PlayersManager instance = null;

    /// <summary>
    /// The order of player for instanciation
    /// </summary>
    private List<int> orderOfPlayer;

    private List<GameObject> players;

    // Use this for initialization
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

        players = new List<GameObject>();

        for (int i = 0; i < orderOfPlayer.Count; i++)
        {
            players.Add(Instantiate(player));
            players[i].GetComponent<PlayerControler>().SetUp(orderOfPlayer[i], orderOfPlayer.Count, i);
        }
    }

    void Update()
    {
        if (players.Capacity == 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    /// <summary>
    /// Define the order of player
    /// </summary>
    public void SetOrderOfPlayer(List<int> order)
    {
        orderOfPlayer = order;
    }

    /// <summary>
    /// Define the cameras' prefab
    /// </summary>
    public void SetCameras(GameObject Cameras)
    {
        this.Cameras = Cameras;
    }
}
