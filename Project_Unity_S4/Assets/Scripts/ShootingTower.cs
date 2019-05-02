using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : MonoBehaviour {

    public float fireRate = 3f;
    public GameObject misslePrefab;
    public float speedOfMissle;
    bool canShoot = true;
    private Transform player;
    Vector2 heading;
    float distance;
    public float maxRange;
    // Use this for initialization
    void Start () {
        //transform.eulerAngles = new Vector3(0, 0, 10);
        
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        heading = transform.position - player.position;
        distance = heading.magnitude;


        StartCoroutine("Shoot");
	}

    IEnumerator Shoot()
    {
        if (canShoot && heading.sqrMagnitude < maxRange*maxRange)
        {
           
            canShoot = false;
            yield return new WaitForSeconds(fireRate);
            GameObject missleClone = Instantiate(misslePrefab, transform.position, transform.rotation);
            Rigidbody2D rb2d = missleClone.GetComponent<Rigidbody2D>();
            rb2d.velocity = -(heading/distance) * speedOfMissle ;
            canShoot = true;
            
        }
    }
    void OnDrawGizmos()
    {
        
        if (player != null&&heading.sqrMagnitude < maxRange * maxRange)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, player.position);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }


}
