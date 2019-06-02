using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float destroyTime;
    public int damage;
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        GameMaster.HurtPlayer(collider, damage);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D objectYouCollidedWith)
    {
        Destroy(this.gameObject);
    }
}
