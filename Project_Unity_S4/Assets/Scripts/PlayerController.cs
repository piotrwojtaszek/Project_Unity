using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {
    protected Rigidbody2D rb2d;
    public float speedForce = 10f;
    public Vector2 jumpForce;
    public bool isGrounded;
    public bool facingRight;

    public Transform groundCheck;
    public float radiuss;
    public LayerMask ground;
    
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
        rb2d.velocity = new Vector2 (moveInput * speedForce,rb2d.velocity.y);

        FlipX(moveInput);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.AddForce(jumpForce, ForceMode2D.Force);
        }



        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiuss, ground);

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(groundCheck.position, radiuss);
    }

}
