using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    GameMaster gm;

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            
            gm.spawnPoint = this.transform;
            col.GetComponent<Player>().Save();
            Debug.Log("przenies");
        }
    }
}
