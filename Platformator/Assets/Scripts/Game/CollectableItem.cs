using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpriteRenderer foodSR;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeExperiencePoints(10);
            audioSource.Play();
            foodSR.enabled = false;
            Debug.Log("Element is collected");
            Destroy(gameObject, 0.4f);
        }
    }
}
