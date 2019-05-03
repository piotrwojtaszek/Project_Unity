using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    public EnemyStats enemyStats;
    // Use this for initialization
    void Start () {
        enemyStats = GetComponent<EnemyStats>();
        enemyStats.Health = enemyStats.maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
		if(enemyStats.Health<= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
