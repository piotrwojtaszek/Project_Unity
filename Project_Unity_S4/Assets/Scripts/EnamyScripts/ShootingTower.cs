using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : MonoBehaviour
{
    private bool canShoot = true;
    private Vector2 heading;
    private float distance;

    public float fireRate = 3f;
    public GameObject misslePrefab;
    public float speedOfMissle;
    public Transform player;
    public float maxRange;
    public EnemyStats enemyStats;
    // Use this for initialization
    void Start()
    {
        //transform.eulerAngles = new Vector3(0, 0, 10);
        enemyStats = GetComponent<EnemyStats>();
        enemyStats.Health = enemyStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyStats.Health <= 0)
        {
            Destroy(this.gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        heading = transform.position - player.position;
        distance = heading.magnitude;


        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        if (canShoot && heading.sqrMagnitude < maxRange * maxRange)
        {

            canShoot = false;
            yield return new WaitForSeconds(fireRate);
            GameObject missleClone = Instantiate(misslePrefab, transform.position, transform.rotation);
            Rigidbody2D rb2d = missleClone.GetComponent<Rigidbody2D>();

            if (distance > maxRange / 2)
            {
                rb2d.velocity = -(heading / distance) * 1.1f * speedOfMissle;
                //Debug.Log(heading.y);
            }
            else if (distance <= maxRange / 2 && distance >= maxRange / 4)
            {
                rb2d.velocity = -(heading / distance) * 0.6f * speedOfMissle;
                //Debug.Log("mniej ni polowa");
            }
            else if (distance <= maxRange / 4)
            {
                //Debug.Log("mniej ni 1/4");
                rb2d.velocity = -(heading / distance) * 0.4f * speedOfMissle;
            }

            if (heading.y > 3f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + heading.y * distance / maxRange / 2);
            }

            canShoot = true;

        }
    }

    void OnDrawGizmos()
    {

        if (player != null && heading.sqrMagnitude < maxRange * maxRange)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, player.position);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }


}
