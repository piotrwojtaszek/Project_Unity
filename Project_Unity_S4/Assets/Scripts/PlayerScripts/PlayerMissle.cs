using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissle : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed;
    [HideInInspector]
    public Transform enemy;
    [HideInInspector]
    public int damage;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 3f);
        rb2d = GetComponent<Rigidbody2D>();
        
    }
	
	// Update is called once per frame
	void Update () {
        

        if (enemy)
        {
            FlyIntoEnemy(enemy);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        
	}
    

    public void FlyIntoEnemy(Transform enemy)
    {
        
        float directionX =  enemy.position.x - transform.position.x;
        float directionY =  enemy.position.y - transform.position.y + enemy.localScale.y;
        directionY = Mathf.Clamp(directionY, -1f, 1f);
        
        transform.Translate(Vector2.up * directionY * Time.deltaTime * speed);

        transform.Translate(Vector2.right * directionX * Time.deltaTime * speed);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            enemy.GetComponent<EnemyStats>().Health -= damage;
            Destroy(this.gameObject);
        }
    }

}
