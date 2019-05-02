using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy : MonoBehaviour {

    // TO DO -> musi dostac detekcje ziemi, dodac do if(isGrounded ) moze skakac

    public bool canAttack = true;
    private Transform player;
    private Vector2 heading;
    private float distance;
    public float maxRange;
    public float attackRate;
    Rigidbody2D rb2d;
    public bool isGrounded;
    public Transform groundCheck1, groundCheck2;
    public LayerMask groundMask;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        heading = transform.position - player.position;
        distance = heading.x;
        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, groundMask);



        StartCoroutine("Attack");
        //Debug.Log(distance);

        


    }

    IEnumerator Attack()
    {
        if(heading.sqrMagnitude < maxRange * maxRange)
        {
            if (canAttack)
            {
                if (isGrounded)
                {
                    Debug.Log("Dziala");
                    canAttack = false;
                    yield return new WaitForSeconds(attackRate);

                    rb2d.velocity = new Vector2(rb2d.velocity.x, 15f);
                    canAttack = true;
                }
            }
            
        }   
    }

    void OnDrawGizmos()
    {

        if (player != null )
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, player.position);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
