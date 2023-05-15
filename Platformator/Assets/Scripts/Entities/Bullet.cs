using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifeTime = 3f;
    private float horSpeed = 5f;
    private bool parentFlipedX;
    private int direction = 0;

    public void SetIsFliped(bool isFliped) {
        parentFlipedX = isFliped;
        gameObject.GetComponent<SpriteRenderer>().flipX = !isFliped;
    }

    private void Start() {
        Rigidbody2D bulletRb = gameObject.GetComponent<Rigidbody2D>();
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
            int sumHit = 1;
            if (other.gameObject.GetComponent<Player>().stats.selectedPerks.IndexOf("Normal mod") != -1) 
                sumHit++;
            if (other.gameObject.GetComponent<Player>().stats.selectedPerks.IndexOf("Hard mod") != -1) 
                sumHit++;
            other.gameObject.GetComponent<Player>().ChangeHealthPoints(-sumHit);
        } else if (other.gameObject.tag == "Wall") {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "RoomShell") {
            gameObject.SetActive(false);
        }
    }
}
