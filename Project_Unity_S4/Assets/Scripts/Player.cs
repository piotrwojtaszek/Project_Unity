using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
        playerStats.Health = playerStats.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [System.Serializable]
    public class PlayerStats                                    // ZYCIE GRACZA
    {
        public int maxHealth = 100;
        public int Health;
    }


    public PlayerStats playerStats = new PlayerStats();
}
