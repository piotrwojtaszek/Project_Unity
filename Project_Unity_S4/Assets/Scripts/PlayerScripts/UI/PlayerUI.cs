using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Image image;
    [SerializeField]private float fillAmount;

    [SerializeField] private Player player;

	// Update is called once per frame
	void Update () {
        HealthBar();
	}

    void HealthBar()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }else if (player != null)
        {
            //Debug.Log(player.playerStats.Health);
            fillAmount = ((float)player.playerStats.Health / (float)player.playerStats.maxHealth);
            image.transform.localScale =new Vector3(fillAmount, image.transform.localScale.y, image.transform.localScale.z);
        }
        
    }
}
