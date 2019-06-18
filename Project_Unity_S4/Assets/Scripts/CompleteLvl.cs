using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CompleteLvl : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Ta scena" + SceneManager.GetActiveScene().buildIndex + "ilosc scen : " + SceneManager.sceneCountInBuildSettings);
        if(col.tag == "Player")
        {
            if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                PlayerPresistance.SaveData();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }
}
