using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Rigidbody2D rb2d;
    public float radius;
    Collider2D[] collider;
    public float fireRate;
    private bool isCorutinePlay;
    public float basicDamage;

    private float damage;
    public float maxDamage;
    public float speedOfIncrasingDmg;

    public bool canShoot;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        GameMaster.shootingSkill = canShoot;

        if (GameMaster.shootingSkill)
        {
            collider = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D col in collider)
            {
                if (col.tag == "Enemy")
                {
                    if (Input.GetButtonUp("Fire1"))
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
            Debug.Log("You cant shoot");
        }
    }

    void Attack(Collider2D col)
    {
        Debug.DrawLine(transform.position, col.transform.position, Color.red, 0.2f);



        col.GetComponent<EnemyStats>().Health -= (int)damage;
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
        if (Input.GetButton("Fire1"))
        {

            damage += Time.deltaTime * speedOfIncrasingDmg;
            damage = Mathf.Clamp(damage, basicDamage, maxDamage);

        }
        else
        {

            if (damage != basicDamage)
              //  Debug.Log(damage);
            damage = basicDamage;
        }
    }
}
