using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerPresistance{

    public static void SaveData(Player player)
    {
        GameMaster gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        PlayerPrefs.SetFloat("x", gm.spawnPoint.transform.position.x);
        PlayerPrefs.SetFloat("y", gm.spawnPoint.transform.position.y);
        PlayerPrefs.SetFloat("z", gm.spawnPoint.transform.position.z);
        PlayerPrefs.SetInt("scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("health", player.playerStats.Health);
    }

    public static void SaveData()
    {
        PlayerPrefs.SetFloat("x", 0);
        PlayerPrefs.SetFloat("y", 0);
        PlayerPrefs.SetFloat("z", 0);
        PlayerPrefs.SetInt("scene", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("health", 100);
    }

    public static PlayerData LoadData()
    {
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        float z = PlayerPrefs.GetFloat("z");
        int _scene = PlayerPrefs.GetInt("scene");
        int _health = PlayerPrefs.GetInt("health");

        if(SceneManager.GetActiveScene().buildIndex != _scene && SceneManager.GetActiveScene().buildIndex != 0)// przydaje sie do tesowania sceny, bo tak to spawnuje gracza w miejscu z innej sceny
        {
            GameMaster gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            x = gm.spawnPoint.transform.position.x;
            y = gm.spawnPoint.transform.position.y;
            z = gm.spawnPoint.transform.position.z;
        }
        PlayerData playerData = new PlayerData()
        {
            location = new Vector3(x, y, z),
            scene = _scene,
            health = _health
        };
        
        return playerData;
    }
}
