using UnityEngine;

public class WhenPlayerIsNear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "PlayerField" || other.gameObject.tag == "RoomPoint") {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerField") {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
