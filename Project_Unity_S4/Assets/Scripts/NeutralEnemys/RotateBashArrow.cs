using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBashArrow : MonoBehaviour {
    private Vector3 worldMousePosition;

    // Update is called once per frame
    void Update () {
        worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        transform.rotation = Quaternion.LookRotation(Vector3.forward, worldMousePosition - transform.position);
        if(Time.timeScale == 1f)
        {
            SelfDestroyArrow();
        }
    }

    public void SelfDestroyArrow()
    {
        Destroy(this.gameObject);
    }
}
