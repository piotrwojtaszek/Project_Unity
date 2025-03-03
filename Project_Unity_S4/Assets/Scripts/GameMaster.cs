﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster gm;
    public Transform playerPrefab;
    public Transform cubePrefab;
    public Transform spawnPoint;
    private IEnumerator coroutine;
    public static bool shootingSkill = true;
    

    // Use this for initialization
    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        
        RespawnPlayer();
    }

    public static void KillPlayer(Player player, GameObject cuteCube)
    {
        gm.RespawnPlayer();
        Destroy(player.gameObject);
        Destroy(cuteCube.gameObject);
    }




    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        yield return new WaitForSeconds(0f);
        Transform player = (Transform)Instantiate(playerPrefab, PlayerPresistance.LoadData().location, spawnPoint.rotation);
        Transform cube = (Transform)Instantiate(cubePrefab, PlayerPresistance.LoadData().location, spawnPoint.rotation);
        CameraFollow cameraFolow = Camera.main.GetComponent<CameraFollow>();
        cameraFolow.target = player;
    }

    public static void ColorOnDamage(Player go)
    {

        gm.coroutine = gm.ColorOnDamageCo(go);
        gm.StartCoroutine(gm.coroutine);

    }

    IEnumerator ColorOnDamageCo(Player go)
    {

        Renderer render = go.GetComponent<Renderer>();
        try
        {
            render.material.color = Color.red;
        }
        catch (MissingReferenceException)
        {
            //Debug.Log("jest okej1");
        }
        yield return new WaitForSeconds(.2f);
        try
        {
            render.material.color = Color.white;
        }
        catch (MissingReferenceException)
        {
            //Debug.Log("jest okej2");
        }                                                               //obsluga wyjatkow, probowal sie dostac do obiektu, a on juz byl zniszoczny(gra sie nei wywala ale po co miec te bledy)

    }

    public static void HurtPlayer(Collider2D collider, int damage)           // po co za kazdym razem to pisac skoro mozna zrobic z tego metode :) 
    {
        if (collider.tag == "Player")
        {
            Player player = collider.GetComponent<Player>();
            
            player.playerStats.Health -= damage;
        }
    }

    IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(.5f);
    }

    
}
