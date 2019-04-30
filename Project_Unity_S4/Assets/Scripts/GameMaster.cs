using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    private GameMaster gm;
    public Transform playerPrefab;
    public Transform spawnPoint;
    
    // Use this for initialization
    void Start () {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }

        
        RespawnPlayer();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
    }

    public void RespawnPlayer()
    {
        Transform player = (Transform)Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        CameraFollow cameraFolow = Camera.main.GetComponent<CameraFollow>();
        cameraFolow.target = player;


    }
}
