using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseClick : MonoBehaviour {

    public GameObject objectPrefab;
    private Vector3 mousePos;
    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            CreateObject(Input.mousePosition);
    }

    public void CreateObject(Vector2 mousePosition)
    {
        mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
        GameObject obj = (GameObject)Instantiate(objectPrefab, mousePos, transform.rotation);
        //Debug.Log(mousePos);
    }

}
