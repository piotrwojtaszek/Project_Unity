using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePowerUps : MonoBehaviour {
    
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private int howMany = 1;
	void OnDestroy()
    {
        int health = GetComponent<EnemyStats>().Health;
        if(health <= 0)
        {
            Debug.Log("zabiles mnie brutalu");
            Create();
        }
        
    }

    void Create()
    {
       
       for (int i = 0; i < howMany; i++)
        {
            GameObject powerUpCopy = Instantiate(powerUpPrefab, transform.position, transform.rotation);
            Rigidbody2D rb2d = powerUpCopy.GetComponent<Rigidbody2D>();

            rb2d.velocity = new Vector2(Random.Range(-0.5f,0.5f), Random.Range(2f, 7f));
        }
    }
}
