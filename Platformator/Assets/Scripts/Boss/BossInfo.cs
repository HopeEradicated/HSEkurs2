using UnityEngine;
using UnityEngine.UI;

public class BossInfo : MonoBehaviour
{
    private float primaryHealth;
    private float health = 100f;
    [SerializeField] private Image BossHealthBar;

    private void Start() {
        primaryHealth = health;
    }

    public void ChangeBossHealth(float number) {
        health += number;
        FillHealthBar();
    }

    private void FillHealthBar() {
        BossHealthBar.fillAmount = health / primaryHealth;
    }
}
