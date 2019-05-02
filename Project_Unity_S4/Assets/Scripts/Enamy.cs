using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy : MonoBehaviour {

    // TO DO -> musi dostac detekcje ziemi, dodac do if(isGrounded ) moze skakac

    
    public float maxRange;
    public float attackRate;
    public Transform groundCheck1, groundCheck2;
    public LayerMask groundMask;
    public Transform homePosition;
    public float maxDistanceFromHome;
    public float returnSpeed;


    Rigidbody2D rb2d;
    private float distanceFromHome;
    private Transform player;
    private Vector2 heading;
    private float distance;
    private float oldGravityScale;
    private bool isCourotinePlay;
    private bool isGrounded;
    private bool canAttack = true;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        oldGravityScale = rb2d.gravityScale;
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        heading = transform.position - player.position;
        distance = heading.x;
        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, groundMask);


        Vector2 vectorBeetwenHome = transform.position - homePosition.position;
        distanceFromHome = vectorBeetwenHome.magnitude;


        if (distanceFromHome < maxDistanceFromHome)
        {
            StartCoroutine("Attack");
        }
        else if(!isCourotinePlay && Mathf.Abs(distanceFromHome) > 2f)
        {
            if (isGrounded)
            {
                transform.Translate(Vector2.right * -Mathf.Sign(vectorBeetwenHome.x) * returnSpeed * Time.deltaTime);
            }
            
        }
        

        if (!isGrounded)
        {
             
            if (rb2d.velocity.y <= 0.5f)
            {
                rb2d.gravityScale = 4;
            }
            else
            {
                rb2d.gravityScale = oldGravityScale;
            }
            

            transform.Translate(Vector2.right *- heading*2 * Time.deltaTime);
        }
        Debug.Log(distanceFromHome);


    }

    IEnumerator Attack()
    {
        if (Mathf.Abs( distanceFromHome )<= maxDistanceFromHome)
        {
            if (heading.sqrMagnitude < maxRange * maxRange)
            {
                if (canAttack)
                {
                    if (isGrounded)
                    {
                        isCourotinePlay = true;
                           //Debug.Log("Dziala");
                           canAttack = false;
                        yield return new WaitForSeconds(attackRate);

                        rb2d.velocity = new Vector2(rb2d.velocity.x, 15f);


                        isCourotinePlay = false;
                        canAttack = true;
                    }
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
