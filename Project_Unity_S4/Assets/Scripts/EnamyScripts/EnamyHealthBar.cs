using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyHealthBar : MonoBehaviour
{
    float maxHealth, Health;

    EnemyStats enemyStats;

    void Update()
    {
        enemyStats = gameObject.GetComponentInParent(typeof(EnemyStats)) as EnemyStats;

        Health = enemyStats.Health;
        maxHealth = enemyStats.maxHealth;
        SetSize(Health, maxHealth);
    }

    void SetSize(float sizeNormalized, float max)
    {
        this.transform.localScale = new Vector3(sizeNormalized / max, 1f);
    }
}
