using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifeTime = 3f;
    private float horSpeed = 5f;
    private bool parentFlipedX;
    private int direction = 0;

    [SerializeField]
    private SpriteRenderer bulletSpriteRenderer;

    public void SetIsFliped(bool isFliped) {
        parentFlipedX = isFliped;
    }

    private void Start() {
        Rigidbody2D bulletRb = gameObject.GetComponent<Rigidbody2D>();
        //parentFlipedX = gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().flipX;

        if (parentFlipedX) {
            direction = 1;
        } else {
            direction = -1;
        }

        bulletRb.velocity = new Vector2(direction * horSpeed, bulletRb.velocity.y);
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("Hit the player");
        } else if (other.gameObject.tag != "Shooter" && other.gameObject.tag != "CollectiveElem") {
            bulletSpriteRenderer.enabled = false;
        }
    }
}
