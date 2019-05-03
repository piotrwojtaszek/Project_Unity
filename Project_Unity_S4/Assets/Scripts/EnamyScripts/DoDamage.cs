using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    public int damage = 10;


    void OnTriggerEnter2D(Collider2D collider)
    {
        GameMaster.HurtPlayer(collider, damage);
    }
}