using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour {
    //TO DO -> zrobic przeciwnika w ktroego tzreba bedzie strzelac tymi pociskami :) 
    public float radius;
    private Collider2D[] collider;
    private Rigidbody2D rb2d;
    public float dashForce;
    private bool coroutine;
    private Vector3 direction;
    private Vector3 worldMousePosition;
    private bool canDash = true;
    public float missleSpeed;
	// Use this for initialization
	void Start () {
        
        rb2d = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {

        worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        
        //Debug.Log((Vector2)direction);
        collider = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D col in collider)
        {
            if(col.tag == "DashAbleMissle")
            {
                
                Dash(col);
                
            }
        }
        //Debug.Log(canDash);
	}

    void Dash(Collider2D col)
    {
        bool isInAir = GetComponent<PlayerController>().isInAir;
        if (isInAir && canDash)
        {
            if (Input.GetButtonDown("Jump"))
            {
                StartCoroutine("CollisionTime");
                direction = worldMousePosition - col.transform.position;
                direction.Normalize();
                MoveMissle(col);
                

            }
            if (Input.GetButtonUp("Jump") && coroutine)
            {
                StopCoroutine("CollisionTime");
                Time.timeScale = 1f;
                direction = worldMousePosition - col.transform.position;
                direction.Normalize();
                DashForce();
                MoveMissle(col);
                
            }

        }
    }

    void DashForce()
    {
        rb2d.AddForce(Vector2.up * dashForce);
        StartCoroutine("TimeToDashSkill");
    }

    IEnumerator CollisionTime()
    {
        coroutine = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        DashForce();
        
    }

    IEnumerator TimeToDashSkill()
    {
        canDash = false;
        yield return new WaitForSecondsRealtime(0.8f);
        canDash = true;
    }

    void MoveMissle(Collider2D col)
    {

        Rigidbody2D rb2dMissle = col.GetComponent<Rigidbody2D>();
        rb2dMissle.velocity = -direction * missleSpeed;
        //col.transform.tag = "UnDashAbleMissle";
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
