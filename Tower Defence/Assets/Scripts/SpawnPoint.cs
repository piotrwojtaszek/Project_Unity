using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool coroutine;
    public float waitTime;
    public GameObject enemyPrefab;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (coroutine == false)
        {
            StartCoroutine("SpawnEnemy");
        }
    }

    IEnumerator SpawnEnemy()
    {
        coroutine = true;
        Instantiate(enemyPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(waitTime);
        coroutine = false;
    }
}
