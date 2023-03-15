using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int healthPoints = 3;
    private int expPoints = 0;

    [SerializeField] private List<GameObject> healthIcons;

    public void ChangeHealthPoints(int number) {
        if (healthPoints + number >= 0 && healthPoints + number <= 3) {
            UpdateHealthBar(healthPoints + (number - 1 * Mathf.Abs(number)) / 2, (number > 0));
            healthPoints += number;
        }
    }

    public void ChangeExperiencePoints(int number) {
        expPoints += number;
    }

    private void UpdateHealthBar(int index, bool isActive) {
        healthIcons[index].SetActive(isActive);
    }

    public bool isHealhEqualToZero() {
        return (healthPoints == 0);
    }
}
