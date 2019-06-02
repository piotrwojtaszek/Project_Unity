using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowinMissle : MonoBehaviour {
    public GameObject misslePrefab;
    private bool coroutine;
    public float missleSpeed;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if(coroutine == false)
        {
            StartCoroutine("RespawnMissle");
        }

	}

    IEnumerator RespawnMissle()
    {
        coroutine = true;
        GameObject obj = (GameObject)Instantiate(misslePrefab, transform.position, transform.rotation);
        Rigidbody2D rb2dObj = obj.GetComponent<Rigidbody2D>();
        rb2dObj.velocity = new Vector2(1f,1f) * missleSpeed;
        yield return new WaitForSeconds(3f);
        coroutine = false;
    }
}
