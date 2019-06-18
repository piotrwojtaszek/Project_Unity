using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    GameMaster gm;
    public SpriteRenderer som;
	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            //som = GetComponent<SpriteRenderer>();
            //som.enabled = true;
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            
            gm.spawnPoint.position = this.transform.position;
            col.GetComponent<Player>().Save();
            
            Debug.Log("przenies");
        }
    }
}
