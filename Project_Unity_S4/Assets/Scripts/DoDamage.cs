using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    public int damage = 10;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Player player = collider.GetComponent<Player>();
            Debug.Log(player.playerStats.Health);
            player.playerStats.Health -= damage;
            Debug.Log(player.playerStats.Health);
            
        }
    }
}