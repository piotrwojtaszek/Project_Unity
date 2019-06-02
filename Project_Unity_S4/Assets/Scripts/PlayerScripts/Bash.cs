using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
{

    public GameObject arrowPrefab;
    private GameObject arrow;
    public float radius;
    private Collider2D[] collider;
    private Rigidbody2D rb2d;
    public float dashForce;
    private bool coroutine;
    private Vector3 direction;
    private Vector3 worldMousePosition;
    private bool canDash = true;
    public float missleSpeed;
    private bool clickedIn = false;         // pomaga wyeliminowac blad polegajacy na : gdy gracz robil ButtonUp to nawet gdy nie bylo ButtonDown
                                            // Use this for initialization          // w obrebie mozliwej kolizji z pociskiem ten i tak dizlal na niego i pocisk lecial i lecial
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        //Debug.Log((Vector2)direction);
        collider = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D col in collider)
        {
            if (col.tag == "DashAbleMissle")
            {

                Dash(col);
                
            }
        }
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
                clickedIn = true;
                DrawDirection(col);
            }
            if (Input.GetButtonUp("Jump") && coroutine && clickedIn)
            {
                StopCoroutine("CollisionTime");
                Time.timeScale = 1f;
                direction = worldMousePosition - col.transform.position;
                direction.Normalize();
                rb2d.velocity = Vector2.zero;
                Corners(col);
                DashForce();
                MoveMissle(col);
                clickedIn = false;
                DrawDirection(col);

            }
        }
    }

    void DashForce()
    {
        Vector2 dashForceY =Vector2.up* direction.y * dashForce;
        Vector2 dashForceX = Vector2.right * direction.x * dashForce;
        Vector2 dashDirectionVec = new Vector2(dashForceX.x, dashForceY.y);

        rb2d.AddForce(dashDirectionVec);
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
        yield return new WaitForSecondsRealtime(0.5f);
        canDash = true;
    }

    void MoveMissle(Collider2D col)
    {
        Rigidbody2D rb2dMissle = col.GetComponent<Rigidbody2D>();
        rb2dMissle.velocity = -direction * missleSpeed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    void Corners(Collider2D col)
    {
        Vector2 targetPos = new Vector2(col.bounds.center.x + Mathf.Sign(direction.x) * 1.5f, col.bounds.center.y + Mathf.Sign(direction.y) * 1.5f);
        transform.position = targetPos;
    }

    void DrawDirection(Collider2D col)
    {
        Vector2 center = new Vector2(col.bounds.center.x, col.bounds.center.y);
            GameObject arrow = Instantiate(arrowPrefab, center, col.transform.rotation);
    }

}
