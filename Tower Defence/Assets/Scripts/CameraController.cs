using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Move(moveInput);
	}

    void Move(Vector2 moveInput)
    {
        transform.Translate(new Vector3(moveInput.x * speed * Time.deltaTime, moveInput.y * speed * Time.deltaTime, 0f));
    }
}
