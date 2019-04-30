using UnityEngine;
using System.Collections;
/*
 * 
 * JAK ZACHOWUJE SIE KAMERA
 * 
 */

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // DO SZEGO PRZYCZEPIAMY
    public float smoothing = 5f;        // JAK GLADKO MA SIE PORUSZAC
    public float smoothSpeed = 2.0f;
    public Vector3 offset;


    public void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }


        offset = transform.position + target.position;                          

                                    
    }


    void FixedUpdate()
    {

        if (target == null)                                                     
            return;

        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        

    }
}