using UnityEngine;

public class TriggerOff : MonoBehaviour
{
    private BoxCollider2D col2D;

    private void Start() {
        col2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (transform.position.y < other.gameObject.transform.localPosition.y) {
            col2D.isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        col2D.isTrigger = true;
    }
}
