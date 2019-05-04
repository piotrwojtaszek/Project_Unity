using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : MonoBehaviour
{

    Rigidbody2D rb2d;
    Collider2D[] collider;
    public float radius;
    public float wallCheckRadius;
    public Transform wallCheck;

    public bool isWalled;
    public LayerMask whatIsWall;
    private int facingRight;
    public float acceleration;
    public float maxSpeed;
    public float walkSpeed;
    public Transform attackPointA, attackPointB;
    public static bool playerInRange;
    private bool firstEntrance;

    EnemyStats enemyStats;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        facingRight = 1;
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

        if (playerInRange)
        {
            Attack();
            firstEntrance = false;
        }
        else
        {
            firstEntrance = true;
            Walk();
        }

        isWalled = Physics2D.OverlapCircle(wallCheck.transform.position, wallCheckRadius, whatIsWall);

        if (isWalled)
        {
            FlipX();
            isWalled = false;
            rb2d.velocity = Vector2.zero;
        }

        rb2d.AddForce(new Vector2(acceleration * facingRight, 0));

        rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed), rb2d.velocity.y);
    }


    void FlipX()
    {

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (wallCheck.transform.position.x > transform.position.x)
        {
            facingRight = 1;
        }
        else if (wallCheck.transform.position.x < transform.position.x)
        {
            facingRight = -1;
        }
    }

    void Attack()
    {
        Debug.Log("Atakuje");

        if (firstEntrance)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            if(player.position.x > this.transform.position.x && facingRight == -1)
            {
                FlipX();
            }else if(player.position.x < this.transform.position.x && facingRight == 1)
            {
                FlipX();
            }
        }

    }

    void Walk()
    {
        Debug.Log("Spaceruje");

        rb2d.velocity = new Vector2(walkSpeed * facingRight, rb2d.velocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(attackPointA.transform.position, new Vector3(attackPointB.transform.position.x, attackPointA.transform.position.y, 0f));
        Gizmos.DrawLine(new Vector3(attackPointA.transform.position.x, attackPointB.transform.position.y), attackPointB.transform.position);
        Gizmos.DrawLine(attackPointA.transform.position, new Vector3(attackPointA.transform.position.x, attackPointB.transform.position.y, 0f));
        Gizmos.DrawLine(attackPointB.transform.position, new Vector3(attackPointB.transform.position.x, attackPointA.transform.position.y, 0f));

        Gizmos.DrawWireSphere(wallCheck.transform.position, wallCheckRadius);
    }
}
