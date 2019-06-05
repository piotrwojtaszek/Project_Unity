using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    private float timeToDestroy;
    private int addHealth;

    void Start()
    {
        timeToDestroy = Random.Range(8f, 12f);
        addHealth = Random.Range(2, 8);
        Destroy(this.gameObject, timeToDestroy);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            
            Player player = col.GetComponent<Player>();
            if(player.playerStats.Health == player.playerStats.maxHealth)
            {
                Destroy(this.gameObject);
            }
            else if(player.playerStats.Health + addHealth >= player.playerStats.maxHealth)
            {
                player.playerStats.Health = player.playerStats.maxHealth;
                Destroy(this.gameObject);
            }
            else
            {
                player.playerStats.Health += addHealth;
                Destroy(this.gameObject);
            }
            
        }
    }
}
