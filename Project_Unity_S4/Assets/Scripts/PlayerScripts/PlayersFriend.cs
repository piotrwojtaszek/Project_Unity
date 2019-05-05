using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersFriend : MonoBehaviour {

    private GameObject player;
    public float smoothing;
    float velocityY;
    Vector2 playerVelocity;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        velocityY = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        

        if(playerVelocity.y < 0)
        {
            velocityY = Mathf.Lerp(velocityY, 0f, 1f);
        }
        else if(playerVelocity.y > 0)
        {
            velocityY = Mathf.Lerp(velocityY, playerVelocity.y/3, 1f);
        }
        else
        {
            velocityY = 0;
        }

       
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f + velocityY, player.transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);

    }
}
