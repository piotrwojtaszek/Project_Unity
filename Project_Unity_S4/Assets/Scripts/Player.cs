using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator animator;
    private PlayerController playerController;

    public float timeFromGrounded;
	// Use this for initialization
	void Start () {
        playerStats.Health = playerStats.maxHealth;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

        
        animator.SetFloat("Speed",Mathf.Abs( playerController.rb2d.velocity.x));



        if (playerController.isGrounded )
        {
                timeFromGrounded = 0;  
        }else if (playerController.isInAir)
        {
            timeFromGrounded += Time.deltaTime;
        }

        if (playerController.isInAir )
        {
            if(timeFromGrounded > 0.2)
            animator.SetBool("IsInAir", true);
            
        }else if (playerController.isGrounded)
        {
            animator.SetBool("IsInAir", false);
        }

        if (playerController.wallSliding)
        {
            timeFromGrounded = 0;
            animator.SetBool("IsSliding", true);
        }
        else
        {
            animator.SetBool("IsSliding", false);
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
