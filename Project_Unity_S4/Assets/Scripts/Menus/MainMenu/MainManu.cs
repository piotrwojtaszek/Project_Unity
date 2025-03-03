﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour {

    PlayerData playerData;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPresistance.SaveData();
    }

    public void Continue()
    {
        playerData = PlayerPresistance.LoadData();
        SceneManager.LoadScene(playerData.scene);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("LET ME OUT");
    }
}
