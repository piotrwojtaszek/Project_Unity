using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : MonoBehaviour {

    public Collider2D enemy;
    public float missleSpeed = 5f;
    public int damage;
    // Use this for initialization
    
	
	// Update is called once per frame
	void Update () {
        
        if (enemy)
        {
            Attack(enemy);
        }
        else
        {
            Destroy(this.gameObject);
        }
        

        
	}

    void Attack(Collider2D col)
    {
        Vector3 colPos = col.GetComponent<Transform>().position;
        Vector3 heading = colPos - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        transform.Translate(new Vector3(direction.x * missleSpeed * Time.deltaTime, direction.y * missleSpeed * Time.deltaTime, 0f));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            col.GetComponent<EnemyStats>().Health -= damage;
            Destroy(this.gameObject);
        }
    }
}
