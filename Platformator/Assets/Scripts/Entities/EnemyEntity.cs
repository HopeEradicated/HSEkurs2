using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public float health;
    public float value;
    private float riskCoef;

    public void ChangeHealthPoints(float value) {
        health += value;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
