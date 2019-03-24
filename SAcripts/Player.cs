using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [HideInInspector]
    public Rigidbody2D rb;

    private bool isGrounded; // przechowuje informacje o tym czy stykamy sie z powierzchnia po ktorej mozemy skakac
    private bool isWalled; // bez sensu nazwa --> zmienic potem
    private float wallDirX;
    public float dashTime=0.5f;
    
    bool isCorutineRunning;

    public float angle;
    public Vector2 wallJumpForce;
    public float speedVelocityX;
    public float maxSpeedX;
    public float gravity;
    public float radiusCheck;
    public float jumpForce;
    public float slideSpeed;
    

    public LayerMask whatIsGround; // jakie warstwy uznajemy za podloze od ktorego mozemy sie odbijac--> isGrounded = true
    public Transform groundCheck;
    public LayerMask whatIsGoodWall; // od jakich powierzchni mozemy sie odbijac --> moze od kazdej tylko ostawic tak powierzchnie zeby sie nie bawic
    public Transform wallCheck;
    
    [HideInInspector]
    public float moveInput;
    [HideInInspector]
    public bool facingRight = true;


    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float faceDirX=0;
        

        if (facingRight)
        {
            faceDirX = 1;
            //Debug.Log("1");
        }

        else if (!facingRight)
        {
            faceDirX = -1;
            //Debug.Log("-1");
        }

        Vector2 curentSpeed = rb.velocity;

        // obsuga zdarzenia, jesli zostaly wcisniete klawisze
        moveInput = Input.GetAxis("Horizontal");


        // bool majacy w sobie informacje czy mamy kolizje z podlozem
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, whatIsGround);
        isWalled = Physics2D.OverlapCircle(wallCheck.position, radiusCheck, whatIsGoodWall);
        // nadawanie predkosci postaci, Vector2(moveinput * public predkosc, bez zmian)
        //rb.velocity = new Vector2(moveInput * speedVelocityX, rb.velocity.y);

        rb.AddForce(Vector2.right * moveInput*speedVelocityX);

        //rb.velocity =new Vector2( Mathf.Clamp(rb.velocity.x, -maxSpeedX, maxSpeedX),rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);



        // sprawdzanie czy mamy kontakt ze sciana
        isWalled = Physics2D.OverlapCircle(wallCheck.position, radiusCheck, whatIsGoodWall);



        // input odpowiedzialny za skok
        if (isGrounded)
        {
            gravity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

        }

        // sposob na grawitacje --> do zmienienia
        if (!isGrounded)
        {
            if(gravity == 0)
            {
                gravity = 0.33f;
            }
            gravity = gravity + Time.fixedDeltaTime;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y-gravity);
        }

        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

        if (isWalled)
        {
            gravity = 0.5f;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(-moveInput*wallJumpForce.x, wallJumpForce.y);
                //rb.AddForce(Vector2.up * wallJumpForce);
                Debug.Log("Ściana");
            }
            
        }
    
       


            // wywolanie metody flip --> opis metody poniezej
            if (facingRight==false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput<0)
        {
            Flip();
        }

        gravity = Mathf.Clamp(gravity, -2, 2);


        if (Input.GetKey(KeyCode.LeftShift) && dashTime > 0)
        {
            
            rb.AddForce(Vector2.right * moveInput * speedVelocityX / 4);
            dashTime -= Time.fixedDeltaTime;
            //rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeedX, maxSpeedX), rb.velocity.y);
        }
        else
        {
            dashTime = Mathf.Clamp(dashTime, 0, .5f);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeedX, maxSpeedX), rb.velocity.y);
            if (isCorutineRunning == false)
                StartCoroutine(DashRegeneration());
            //dashTime = .5f;
        }
    }

    // metoda odpowiedzalna za obracanie sprita w kierunku ruchu
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


    IEnumerator DashRegeneration()
    {
        isCorutineRunning = true;
        yield return new WaitForSeconds(0.01f);
        Debug.Log("Mozwsz dash");
        dashTime += 0.01f;

        isCorutineRunning = false;
    }

}
