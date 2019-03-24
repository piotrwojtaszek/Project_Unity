using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*          ZAPAMIETAC

    COROUTINE NIE DZIALAJA NA WYLACZONYCH OBIEKTACH... ==> SETACTIVE(FALSE)


    */

public class DoubleJump : MonoBehaviour {

    public GameObject thisObject;
    public float delay;

    private Collider2D col;
    private MeshRenderer ren;

    void Start()
    {
        ren = thisObject.GetComponent<MeshRenderer>();
        col = thisObject.GetComponent<Collider2D>();
    }

    void OnTriggerStay2D(Collider2D c)
    {
       
        if(c.tag == "Player")
        {
           // Debug.Log("okej");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player player;
                
                player = c.GetComponent<Player>();
                player.gravity = 0.33f;
                player.rb.velocity = new Vector2(player.moveInput * 100,player.jumpForce*2.5f);


                DestroyIt();
                StartCoroutine(Wait());
                

            }
            
        }
    }


    //rozwiazanie wyglada calkiem elegancko 
    
    void DestroyIt()
    {
        ren.enabled = false;
        col.enabled = false;
    }

    void RespawnIt()
    {
        ren.enabled = true;
        col.enabled = true;
    }

    IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(delay);
        RespawnIt();
    }
  
}
