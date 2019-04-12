using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {
    protected Rigidbody2D rb2d;
    public float speedForce = 10f;
    public Vector2 jumpForce;
    public bool isGrounded;
    public bool facingRight;

    public Transform groundCheck1;
    public Transform groundCheck2;
    public float radiuss;
    public LayerMask ground;
    float speed;
    // Use this for initialization

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float moveInput = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.A))
        {
            speed = isGrounded ? speedForce : speedForce * 0.8f;
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            speed = isGrounded ? speedForce : speedForce * 0.8f;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
        else if (isGrounded)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        
        

        FlipX(moveInput);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.AddForce(jumpForce, ForceMode2D.Force);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (rb2d.velocity.y > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            }

        }

            isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, ground);

	}

    void FixedUpdate()
    {
        
    }

    void FlipX(float moveInput)
    {
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
    }

    

}
