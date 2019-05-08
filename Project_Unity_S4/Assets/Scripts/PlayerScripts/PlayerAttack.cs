using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Collider2D[] collider;
    private Rigidbody2D rb2d;
    private float damage;
    private bool isCorutinePlay;
    private Vector3 oldScale;
    private float localScaleVariable;
    private SpriteRenderer spriteRend;
    private CircleCollider2D rangeCollider;

    public float fireRate;
    public float basicDamage;
    public float radius;
    public float maxDamage;
    public float speedOfIncrasingDmg;
    public bool canShoot;

    public GameObject misslePrefab;
    private GameObject obj;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        oldScale = transform.localScale;
        spriteRend = GetComponent<SpriteRenderer>();
        rangeCollider = GetComponent<CircleCollider2D>();
        localScaleVariable = oldScale.x;


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(localScaleVariable);
        rangeCollider.radius = radius/localScaleVariable;
        GameMaster.shootingSkill = canShoot;

        if (GameMaster.shootingSkill)
        {
            collider = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D col in collider)
            {
                if (col.tag == "Enemy")
                {
                    if (Input.GetKeyUp(KeyCode.K))
                    {
                        if (isCorutinePlay == false)
                        {
                            Attack(col);
                            StartCoroutine("AttackRate");

                        }
                    }
                }
            }
            IncraseDamage();
        }
        else
        {
            //Debug.Log("You cant shoot");
        }

        ChangeColor();
    }

    void Attack(Collider2D col)
    {
        Debug.DrawLine(transform.position, col.transform.position, Color.red, 0.2f);
        RespawnMissle(col);
        //col.GetComponent<EnemyStats>().Health -= (int)damage;
        //Debug.Log(col.GetComponent<EnemyStats>().Health);
    }

    IEnumerator AttackRate()
    {
        isCorutinePlay = true;

        yield return new WaitForSeconds(fireRate);

        isCorutinePlay = false;
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void IncraseDamage()
    {
        if (Input.GetKey(KeyCode.K))
        {
            localScaleVariable = 0.9f * damage / maxDamage;
            localScaleVariable = Mathf.Clamp(localScaleVariable, oldScale.x, .8f);
            transform.localScale = Vector3.one * localScaleVariable;

            if (damage == maxDamage)
            {
                //Debug.Log(localScaleVariable);
            }
            damage += Time.deltaTime * speedOfIncrasingDmg;
            damage = Mathf.Clamp(damage, basicDamage, maxDamage);
        }
        else
        {
            transform.localScale = oldScale;
            damage = basicDamage;
        }
    }

    private bool enemyInRange;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            enemyInRange = true;
           // Debug.Log("Przeciwnik");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            enemyInRange = true;
            //Debug.Log("Przeciwnik");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            enemyInRange = false;
        }
    }



    void ChangeColor()
    {
        if (enemyInRange)
        {
            //Debug.Log("czerwony");
            spriteRend.material.color = Color.blue;
        }
        else
        {
            //Debug.Log("bialy");
            spriteRend.material.color = Color.white;

        }
    }


    void RespawnMissle(Collider2D col)
    {
        obj = (GameObject)Instantiate(misslePrefab, transform.position, transform.rotation);
        obj.GetComponent<PlayerMissle>().enemy = col.transform;
        obj.GetComponent<PlayerMissle>().damage = (int)damage;
        obj.transform.localScale = Vector3.one * localScaleVariable;
    }
}
