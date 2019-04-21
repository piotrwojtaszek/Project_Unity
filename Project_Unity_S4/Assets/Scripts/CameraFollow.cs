using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// skrypt odpowiedzlany za sledzenie gracza przez kamere

public class CameraFollow : MonoBehaviour
{

    Transform target;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset;
    private Vector3 offsetLeft, offsetRight; // TO DO --> stowrzyc dla tego strukture bo brzydko wyglada 
    private PlayerController player;
    Camera cam;


    // Use this for initialization
    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // ustawianie offset dla kamery, zeby mogla sie wychylac w konkretna strone --> może przechowac to w strukturze (?)
        cam = GetComponent<Camera>();
        offsetLeft.x = -offset.x;
        offsetRight.x = offset.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            // przeliczanie wielkosci kamery na punkty w ukladzie kartezjanskim
            Vector3 point = cam.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));


            // if odpowiedzialne za wychylenie sie kamery w lewo lub prawo, zgodnie z kierunkiem ruchu postaci
            if (player.facingRight == false)
            {
                offset.x = offsetLeft.x;
                //Debug.Log("lewo");
            }
            else if (player.facingRight == true)
            {
                offset.x = offsetRight.x;
                //Debug.Log("prawo");
            }


            //linijki odpowiedzlane za plynne przejscie kamery z jednej pozycji do drugiej
            Vector3 destination = transform.position + offset + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }else if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

    }
}