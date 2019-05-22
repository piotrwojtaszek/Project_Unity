using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour {
    public float range;
    private bool coroutine;
    public float fireRate;
    public GameObject misslePrefab;
    public int damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Collider2D[] colider = Physics2D.OverlapCircleAll(transform.position, range);
        foreach(Collider2D col in colider)
        {
            if(col.tag == "Enemy")
            {
                
                if(coroutine == false)
                {

                    CheckEnemyHealth(col);
                }

            }
        }
	}

    IEnumerator SpawnMissle()
    {
        coroutine = true;
        yield return new WaitForSeconds(fireRate);
        coroutine = false;
    }

    void CheckEnemyHealth(Collider2D col)
    {
        int enemyHealth = col.GetComponent<EnemyStats>().Health;

        if(enemyHealth-damage > 0)
        {
            StartCoroutine("SpawnMissle");
            GameObject obj = (GameObject)Instantiate(misslePrefab, transform.position, transform.rotation);
            obj.GetComponent<MissleController>().enemy = col;
            obj.GetComponent<MissleController>().damage = damage;
        }


    }


}
