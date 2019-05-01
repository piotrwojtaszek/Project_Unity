using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Skrypt odpowiedzialny za własciwosci gracza
public class PlayerController : MonoBehaviour {
    //TO DO - za duzo referencji, zrobic strukture lub klase na nie
    [HideInInspector]
    public Rigidbody2D rb2d;
    [HideInInspector]
    public float moveInput;
    public float speedForce = 10f;                      // jak szybko nadajemy postaci predkosc
    public float maxSpeed;
    public Vector2 jumpForce;
    public Vector2 jumpForceWall;                       // jak szybko postac sie wspina/skacze
   // public float groundSlide;

    public bool facingRight;                            // w ktroa strone postac sie patrzy
    public bool isInAir;                                // czyjest w powetrzu, gdy nie ma kolizji z ziemia ani ze sciana

    public Transform groundCheck1;                      // potrzebne to do detekcji kolizji z ziemia
    public Transform groundCheck2;
    public LayerMask groundMask;
    public bool isGrounded;

    public Transform wallCheck;                         // potrzebne w detekcji kolizji ze sciana
    public float wallCheckRadius;
    private bool isWalled;
    public LayerMask wallMask;
    public bool wallSliding;                            // czy sie slizga
    public float wallSlidingSpeed;

    
    public bool canJump;                                // bedzie potrzebne
    float timeOfWall;
    // Use this for initialization
    float yVelocity = 0.0f;
    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    void Start () {
		
	}
    // ####################################################
    // Update is called once per frame
    void Update () {
        
       

        moveInput = Input.GetAxis("Horizontal");                                                // poruszanie postacia
                                                                                                //kiedy gracz moze skakac
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

        if (isGrounded)
        {
            if (moveInput == 0)
            {
                rb2d.drag = 2;
            }
        }
        else
        {
            rb2d.drag = 0;
        }
                                                                                                    //detekcja kolizji ze sciana
        if (!isGrounded)
        {
            isWalled = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallMask);      //co uznajemy za sciane

            if (facingRight && moveInput > 0.1f || !facingRight && moveInput < -0.1f)
            {
                if (isWalled)
                {

                    HandleWallSliding();                                                            //slizganie sie po scianie
                }
            }
            if(isWalled && moveInput == 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlidingSpeed * 4);

                wallSliding = true;
            }
        }

        if (isInAir && moveInput==0)                                                                // spowalnianie gracza w locie
        {
            float newVelocity = Mathf.SmoothDamp(rb2d.velocity.x, 0f, ref yVelocity, 0.3f);
            rb2d.velocity = new Vector2(newVelocity, rb2d.velocity.y);
        }


        if(!isWalled && !isGrounded)                                                
        {
            isInAir = true;
        }
        else if(isWalled || isGrounded)
        {
            isInAir = false;
        }

        if (isWalled)
        {

        }

        if(!isWalled || isGrounded)
        {
            wallSliding = false;
        }
        CanJump();
        WallLeap();
        

    }

    // ####################################################

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, groundMask);       // co uznajemy za ziemie

        FlipX(rb2d.velocity.x, moveInput);                                                                  //obrot postaci

        rb2d.AddForce(Vector2.right * speedForce * moveInput);                                              // nadawanie predkoci posaci

        if (rb2d.velocity.x > maxSpeed)                                                                     // trzymanie predkosci gracza w przedziale
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        else if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }
    // ####################################################
    void HandleWallSliding()                                                                                // metoda odpowiedzialna za zeslizgiwanie sie gracza po scianei
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlidingSpeed);

        wallSliding = true;

        if (Input.GetButtonDown("Jump"))                                                                    // skoki gracza
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


    // ####################################################
    void FlipX(float vel, float moveInput)                                                                  // obracanie gracza w kierunku ruchu i nadawanie wartosci
    {                                                                                                       // zmiennej facingRight ktora podpowiada w ktora strone jest obrocony gracz
        if ((vel >= 0 && moveInput> 0) || (vel == 0 && moveInput > 0))
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

            facingRight = true;
        }
        else if ((vel <= 0 && moveInput < 0) || ( vel == 0 && moveInput < 0))
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

            facingRight = false;
        }
        
    }
    // ####################################################
    void CanJump()
    {
        if (isInAir && timeOfWall <= 0)
        {
            canJump = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            canJump = false;
        }
        if (!isInAir)
        {
            if (isGrounded)
            {
                canJump = true;
            }
            else if (isWalled && !isGrounded)
            {
                canJump = true;
            }
        }
    }
    // ####################################################
    void WallLeap()
    {
        if (isGrounded)
        {
            timeOfWall = 0f;
        }
        else if (!isGrounded)
        {
            if (isWalled)
            {
                timeOfWall = 1f;
            }
            else if (!isWalled && timeOfWall >= -0.1)
            {
                //Debug.Log(timeOfWall);
                timeOfWall = timeOfWall - Time.deltaTime;
            }
        }

        if (canJump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (!isGrounded && !isInAir)
                {
                    if (moveInput == 0 )
                    {
                       // Debug.Log("leapJump input 0");
                        if (facingRight)
                        {
                            rb2d.AddForce(new Vector2(-3, 3) * jumpForceWall);
                        }
                        else if (!facingRight)
                        {
                            rb2d.AddForce(new Vector2(3, 3) * jumpForceWall);
                        }
                    }
                }
                else if (!isWalled && !isGrounded)
                {
                    
                   // Debug.Log("dzialas");
                    if (facingRight)
                    {
                        rb2d.AddForce(new Vector2(3, 3) * jumpForceWall);
                    }
                    else if (!facingRight)
                    {
                        rb2d.AddForce(new Vector2(-3, 3) * jumpForceWall);
                    }
                }
            } 
        }
    }
}
