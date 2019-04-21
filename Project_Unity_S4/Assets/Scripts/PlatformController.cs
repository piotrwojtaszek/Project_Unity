using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//nadawanie postaci gracza takiej samej predkosci co platforma
public class PlatformController : MonoBehaviour {
    //znalezc inne rozwiazanie bo to jest tylko tymczsowe

    Rigidbody2D rb;
    Vector2 velocity;
    

	// Use this for initialization
	void Start () {
        rb = GetComponentInParent<Rigidbody2D>();
	}
	

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            //mało to eleganckie ale dziala
            
            velocity = rb.velocity;
            collider.GetComponent<PlayerController>().rb2d.velocity = new Vector2( velocity.x, collider.GetComponent<PlayerController>().rb2d.velocity.y);

        }
    }
}
