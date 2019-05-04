using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy : MonoBehaviour
{

    // TO DO -> musi dostac detekcje ziemi, dodac do if(isGrounded ) moze skakac


    public float maxRange;
    public float attackRate;
    public Transform groundCheck1, groundCheck2;
    public LayerMask groundMask;
    public Transform homePosition;
    public float maxDistanceFromHome;
    public float returnSpeed;
    public EnemyStats enemyStats;


    private Rigidbody2D rb2d;
    private float distanceFromHome;
    private Transform player;
    private Rigidbody2D playerRb2d;
    private Vector2 heading;
    private float distance;
    private float oldGravityScale;
    private bool isCourotinePlay;
    private bool isGrounded;
    private bool canAttack = true;
    

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        oldGravityScale = rb2d.gravityScale;
        enemyStats = GetComponent<EnemyStats>();
        enemyStats.Health = enemyStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyStats.Health <= 0)
        {
            Destroy(this.gameObject);
        }

        
        playerRb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        
        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, groundMask);

        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, maxRange);
        foreach(Collider2D col in collider)
        {
            if(col.tag == "Player")
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                heading = transform.position - player.position;
                distance = heading.x;

                if (distanceFromHome < maxDistanceFromHome)
                {
                    StartCoroutine("Attack");
                }
            }
        }

        Vector2 vectorBeetwenHome = transform.position - homePosition.position;
        distanceFromHome = vectorBeetwenHome.magnitude;

        if (!isCourotinePlay)
        {

            if (isGrounded)
            {
                if (distanceFromHome >= maxDistanceFromHome / 4)
                    transform.Translate(Vector2.right * -Mathf.Sign(vectorBeetwenHome.x) * returnSpeed * Time.deltaTime);
            }
        }

        if (!isGrounded)
        {

            if (rb2d.velocity.y <= 0.5f)
            {
                rb2d.gravityScale = 6;
            }
            else
            {
                rb2d.gravityScale = oldGravityScale;
            }


            transform.Translate(Vector2.right * -heading * 2 * Time.deltaTime);
        }
    }

    IEnumerator Attack()
    {
        if (Mathf.Abs(distanceFromHome) <= maxDistanceFromHome)
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

                        rb2d.velocity = new Vector2(rb2d.velocity.x + .5f*playerRb2d.velocity.x, 17f);

                        isCourotinePlay = false;
                        canAttack = true;
                    }
                }
            }
        }
    }



    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, player.position);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(homePosition.position, maxDistanceFromHome);
    }
}
