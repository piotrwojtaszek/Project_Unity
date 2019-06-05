using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CompleteLvl : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            PlayerPresistance.SaveData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
