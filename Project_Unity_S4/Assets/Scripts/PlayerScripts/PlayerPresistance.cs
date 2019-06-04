using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerPresistance{

    public static void SaveData(Player player)
    {
        PlayerPrefs.SetFloat("x", player.transform.position.x);
        PlayerPrefs.SetFloat("y", player.transform.position.y);
        PlayerPrefs.SetFloat("z", player.transform.position.z);
        PlayerPrefs.SetInt("scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("health", player.playerStats.Health);
    }

    public static PlayerData LoadScene()
    {
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        float z = PlayerPrefs.GetFloat("z");
        int _scene = PlayerPrefs.GetInt("scene");
        int _health = PlayerPrefs.GetInt("health");

        PlayerData playerData = new PlayerData()
        {
            location = new Vector3(x, y, z),
            scene = _scene,
            health = _health
        };

        return playerData;
    }
}
