using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnZombies : MonoBehaviour {

    public Transform[] spawnPoints;
    public GameObject[] zombies;

    private List<GameObject> zombiesAlive;

    private int[] quantities;
    private float[] delays;
    private int[] bulkSize;

    private float timer = 0f;

    private bool spawnInProgress = false;
    private bool spawnFinishedAndUnresolved = false;

    void Start() {
        int[] qties = { 10, 10, 10, 10 };
        float[] del = { 2, 2, 2, 2 };
        int[] bulk = { 3, 3, 3, 3 };

        zombiesAlive = new List<GameObject>();

        SpawnWave(qties, del, bulk);
    }

    public void SpawnWave(int[] quantities, float[] delays, int[] bulkSize) {
        if (!(spawnPoints.Length == quantities.Length && spawnPoints.Length == delays.Length && spawnPoints.Length == bulkSize.Length))
            return;
        this.quantities = quantities;
        this.delays = delays;
        this.bulkSize = bulkSize;

        this.spawnInProgress = true;
    }

    void Update() {
        if (!spawnInProgress && spawnFinishedAndUnresolved)
        {
            bool isClear = true;
            foreach (var item in zombiesAlive)
            {
                isClear &= !item.GetComponent<AgentScript>().isDead;
            }

            if (isClear)
                SceneManager.LoadScene(2);
        }

        spawnInProgress = false;

        for (int i = 0; i < spawnPoints.Length; ++i) {
            if (quantities[i] == 0)
                continue;

            spawnInProgress = true;

            if (timer != 0 && timer < delays[i]) {
                timer += Time.deltaTime;
                continue;
            }

            int qty = Mathf.Min(quantities[i], Random.Range(1, bulkSize[i]));

            quantities[i] -= qty;

            for(int j = 0; j < qty; ++j) {
                zombiesAlive.Add(Instantiate(zombies[Random.Range(0, zombies.Length)], spawnPoints[i].position, Quaternion.Euler(new Vector3(90, 0, 0))));
            }

            timer = Time.deltaTime;
        }
    }
}
