using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private BoxCollider2D spikeCollider;
    [SerializeField] private Animator spikeAnimator;

    private List<float> sizes = new List<float>() { 0f, 0.04f, 0.049f, 0.059f};
    private List<float> offsets = new List<float>() { 0f, 0.029f, 0.034f, 0.039f};
    private int colliderIndex;

    private void Start() {
        StartCoroutine("ActivateSpike");
    }

    private IEnumerator ActivateSpike() {
        while (true) {
            spikeAnimator.Play("Activation");
            yield return new WaitForSeconds(5f);
        }
    }

    private void ChangeCollider() {
        colliderIndex++;
        if (colliderIndex == 4) {
            colliderIndex = 0;
            spikeCollider.enabled = false;
        } else {
            spikeCollider.enabled = true;
        }
        spikeCollider.offset = new Vector2(0f, offsets[colliderIndex]);
        spikeCollider.size = new Vector2(0.17f, sizes[colliderIndex]);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("Hit the player");
            int sumHit = 1;
            if (other.gameObject.GetComponent<Player>().stats.selectedPerks.IndexOf("Normal mod") != -1) 
                sumHit++;
            if (other.gameObject.GetComponent<Player>().stats.selectedPerks.IndexOf("Hard mod") != -1) 
                sumHit++;
            other.gameObject.GetComponent<Player>().ChangeHealthPoints(-sumHit);
        }
    }
}
