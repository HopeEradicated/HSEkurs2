using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [Header("CollectivePref")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpriteRenderer foodSR;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("More XP") != -1) 
                Player.GetComponent<Player>().ChangeExperiencePoints(10);
            if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("More gold") != -1) 
                Player.GetComponent<Player>().stats.money += 1;
            Player.GetComponent<Player>().stats.money += 1;
            Player.GetComponent<Player>().ChangeExperiencePoints(10);
            audioSource.Play();
            foodSR.enabled = false;
            Destroy(gameObject, 0.4f);
        }
    }
}