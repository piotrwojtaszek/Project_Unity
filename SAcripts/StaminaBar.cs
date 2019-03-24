using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StaminaBar : MonoBehaviour {

    private Player player;
    float maxStamina;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        maxStamina = player.dashTime;
        
        
    }
	
	// Update is called once per frame
	void Update () {
        //this.transform.localScale = new Vector3(maxStamina - player.dashTime, this.transform.localScale.y, this.transform.localScale.z);
        this.SetSize(player.dashTime,maxStamina);
	}

    void SetSize(float sizeNormalized,float max)
    {
        this.transform.localScale = new Vector3(sizeNormalized/max, 1f);
    }
}
