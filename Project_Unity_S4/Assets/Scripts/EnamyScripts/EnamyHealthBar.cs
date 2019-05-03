using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyHealthBar : MonoBehaviour {
    float maxHealth, Health;
    Enamy enamy;

	void Update () {
        enamy = gameObject.GetComponentInParent(typeof(Enamy)) as Enamy;
        Health = enamy.enemyStats.Health;
        maxHealth = enamy.enemyStats.maxHealth;
        Debug.Log(Health);
        SetSize(Health, maxHealth);
	}

    void SetSize(float sizeNormalized, float max)
    {
        this.transform.localScale = new Vector3(sizeNormalized / max, 1f);
    }
}
