using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersFriend : MonoBehaviour {

    private GameObject player;
    public float smoothing;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);

    }

    
}
