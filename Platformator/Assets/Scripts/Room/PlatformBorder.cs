using UnityEngine;

public class PlatformBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 3) {
            SpriteRenderer colliderSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
            colliderSpriteRenderer.flipX = !colliderSpriteRenderer.flipX;
        }
    }
}