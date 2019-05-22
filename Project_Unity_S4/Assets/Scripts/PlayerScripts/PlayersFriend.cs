using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersFriend : MonoBehaviour
{

    private GameObject player;
    public float smoothing;
    float velocityY;
    Vector2 playerVelocity;
    public float radius;
    public float rotateSpeed;
    private float angle;
    private bool wasRunning;
    public float damage;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        velocityY = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity;

        if (playerVelocity.x == 0 && playerVelocity.y == 0)
        {
            OnStay();
        }
        else
        {
            FollowPlayer(playerVelocity);
        }
        



    }

    void FollowPlayer(Vector3 playerVelocity)
    {
        if (playerVelocity.y < 0)
        {
            velocityY = Mathf.Lerp(velocityY, 0f, 1f);
        }
        else if (playerVelocity.y > 0)
        {
            velocityY = Mathf.Lerp(velocityY, playerVelocity.y / 3, 1f);
        }
        else
        {
            velocityY = 0;
        }


        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f + velocityY, player.transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
    }

    void OnStay()
    {
        Vector3 centre = new Vector3(player.transform.position.x, player.transform.position.y + 1f + radius * Time.deltaTime, player.transform.position.z);
        angle += rotateSpeed * Time.deltaTime;

        Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        
        Vector2 targetPos = (Vector2)centre + new Vector2(offset.x, offset.y / 2f);

        transform.position = Vector3.Lerp(transform.position, targetPos, rotateSpeed * Time.deltaTime);
    }

    public void IncrasingRotate(float damage)
    {
        transform.RotateAround(transform.position, Vector3.forward, -damage/2);
    }
}
