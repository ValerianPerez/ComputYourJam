using UnityEngine;
using System.Collections;

public class PlayAnimOnKeyUp : MonoBehaviour {

    public GameObject mainProjectile;
    public ParticleSystem mainParticleSystem;

    float plop = 0;
	// Update is called once per frame
	void Update () {
        plop += Time.deltaTime;


        if (plop > 1)
        {
            plop = 0;
            mainProjectile.SetActive(true);
        }

        if (mainParticleSystem.IsAlive() == false)
            mainProjectile.SetActive(false);
	
	}
}
