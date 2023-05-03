using UnityEngine;
using UnityEngine.UI;

public class BossInfo : MonoBehaviour
{
    private float primaryHealth;
    public float health = 100f;
    [SerializeField] private Image BossHealthBar;

    private void Start() {
        primaryHealth = health;
    }

    public void ChangeBossHealth(float number) {
        health += number;
        FillHealthBar();
        if (isBossHealhEqualLessThanZero()) {
            Die();
        }
    }

    private void FillHealthBar() {
        BossHealthBar.fillAmount = health / primaryHealth;
    }

    public float GetBossHealth() {
        return health;
    }

    public float GetBossPrimaryHealth() {
        return primaryHealth;
    }

    public bool isBossHealhEqualLessThanZero() {
        return (health <= 0f);
    }

    private void Die() {
        Destroy(gameObject);
    }
}
