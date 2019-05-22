using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour {
    private Vector3 basePos;
    public float speed;
    public EnemyStats enemyStats;
	// Use this for initialization
	void Start () {
        basePos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        enemyStats = GetComponent<EnemyStats>();
        enemyStats.Health = enemyStats.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        KillZombie();
        Vector3 heading = basePos - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        transform.Translate(new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0f));
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Debug.Log("niszcz");
            Destroy(this.gameObject);
        }
    }
    
    void KillZombie()
    {
        if(enemyStats.Health<= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
