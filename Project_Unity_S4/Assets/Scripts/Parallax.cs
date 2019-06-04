using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    private float length, startPosX;

    public GameObject cam;

    public float parallaxEffectX;



	// Use this for initialization
	void Start () {
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
	
	// Update is called once per frame
	void Update () {

        float temp = cam.transform.position.x * (1 - parallaxEffectX);

        float dist = (cam.transform.position.x * parallaxEffectX);

        transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);

        if (temp > startPosX + length)
        {
            startPosX += length;
        }
        else if(temp < startPosX - length)
        {
            startPosX -=length;
        }
       
	}
}
