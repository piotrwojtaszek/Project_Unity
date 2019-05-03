using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private GameObject cuteCube;
    private Animator animator;
    private PlayerController playerController;
    private int oldHealth;
    // Use this for initialization
    void Start () {
        cuteCube = GameObject.FindGameObjectWithTag("CuteCube");

        playerStats.Health = playerStats.maxHealth;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        oldHealth = playerStats.Health;
	}
	
	// Update is called once per frame
	void Update () {
        
        animator.SetFloat("Speed",Mathf.Abs( playerController.rb2d.velocity.x));

        animator.SetBool("IsInAir", playerController.isInAir);

        animator.SetBool("IsSliding", playerController.wallSliding);
        animator.SetBool("IsGrounded", playerController.isGrounded);



        if (playerStats.Health <= 0)
        {
            GameMaster.KillPlayer(this, cuteCube);
        }

        //genialna funkcja ktora w przypadku otrzymania JAKICHKOLWIEK obrazen wywołuje metode z GameMastera
        if (playerStats.Health < oldHealth)
        {
            GameMaster.ColorOnDamage(this);
        }
        oldHealth = playerStats.Health;
        //###################


        // zabezbieczenie na wypadek przeniknięcia przez podłoże
        if(transform.position.y < -100)
        {
            GameMaster.KillPlayer(this, cuteCube);
        }
    }

    [System.Serializable]
    public class PlayerStats                                    // ZYCIE GRACZA
    {
        public int maxHealth = 100;
        public int Health;
    }


    public PlayerStats playerStats = new PlayerStats();
}
