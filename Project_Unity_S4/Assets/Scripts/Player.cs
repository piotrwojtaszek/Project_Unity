using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator animator;
    private PlayerController playerController;

	// Use this for initialization
	void Start () {
        playerStats.Health = playerStats.maxHealth;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

        
        animator.SetFloat("Speed",Mathf.Abs( playerController.rb2d.velocity.x));



        animator.SetBool("IsInAir", playerController.isInAir);


        animator.SetBool("IsSliding", playerController.wallSliding);
        animator.SetBool("IsGrounded", playerController.isGrounded);
    }

    [System.Serializable]
    public class PlayerStats                                    // ZYCIE GRACZA
    {
        public int maxHealth = 100;
        public int Health;
    }


    public PlayerStats playerStats = new PlayerStats();
}
