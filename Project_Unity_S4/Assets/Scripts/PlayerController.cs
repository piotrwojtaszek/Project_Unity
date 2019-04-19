using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {

    [HideInInspector]
    public Rigidbody2D rb2d;
    [HideInInspector]
    public float moveInput;
    public float speedForce = 10f;
    public float maxSpeed;
    public Vector2 jumpForce;
    public Vector2 jumpForceWall;
   // public float groundSlide;

    public bool facingRight;
    public bool isInAir;

    public Transform groundCheck1;
    public Transform groundCheck2;
    public LayerMask groundMask;
    private bool isGrounded;

    public Transform wallCheck;
    public float wallCheckRadius;
    private bool isWalled;
    public LayerMask wallMask;
    public bool wallSliding;

    [HideInInspector]
    public bool canJump;
    
    // Use this for initialization

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("Jump") && !wallSliding)
        {
            if (isGrounded)
            {
                rb2d.AddForce(jumpForce, ForceMode2D.Force);
            }   
        }
        else if (Input.GetButtonUp("Jump") && !wallSliding)
        {
            if (rb2d.velocity.y > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            }
        }
    
        if (!isGrounded)
        {
            isWalled = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallMask);

            if (facingRight && moveInput > 0.1f || !facingRight && moveInput < -0.1f)
            {
                if (isWalled)
                {
                    HandleWallSliding();
                }
            }
        }

        if (isInAir && moveInput==0)
        {
            if(rb2d.velocity.x > maxSpeed / 4f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x * 0.98f, rb2d.velocity.y);
            }
            else if(rb2d.velocity.x <= maxSpeed / 4f)
            {
                rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
            }
        }

        

        if (isGrounded)
        {
          // GroundMoveTweek(moveInput);
        }

        if(!isWalled && !isGrounded)
        {
            isInAir = true;
        }
        else if(isWalled || isGrounded)
        {
            isInAir = false;
        }



        if(!isWalled || isGrounded)
        {
            wallSliding = false;
        }
        moveInput = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
       

        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, groundMask);

        FlipX(rb2d.velocity.x, moveInput);

        rb2d.AddForce(Vector2.right * speedForce * moveInput);

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        else if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.5f);

        wallSliding = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-1, 3) * jumpForceWall);
            }
            else
            {
                rb2d.AddForce(new Vector2(1, 3) * jumpForceWall);
            }
        }
    }

    

    void FlipX(float vel, float moveInput)
    {
        if ((vel > 0 && moveInput> 0) || (vel == 0 && moveInput > 0))
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

            facingRight = true;
        }
        else if ((vel < 0 && moveInput < 0) ||( vel == 0 && moveInput < 0))
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

            facingRight = false;
        }
        
    }

    /*void GroundMoveTweek(float moveInput)
    {
        if (moveInput == 0)
        {
            if (rb2d.velocity.x > 1f)
            {
                rb2d.AddForce(Vector2.right * -groundSlide);
            }
            else if (rb2d.velocity.x < -1f)
            {
                rb2d.AddForce(Vector2.right * groundSlide);
            }

            if(rb2d.velocity.x < 1f && rb2d.velocity.x > 0f)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            else if (rb2d.velocity.x > -1f && rb2d.velocity.x < 0f)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }*/


    [System.Serializable]
    public class PlayerStats                                    // ZYCIE GRACZA
    {
        public int Health = 100;
    }


    public PlayerStats playerStats = new PlayerStats();
}
