using UnityEngine;

public class WhenPlayerIsNear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerField") {
            Debug.Log("1");
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerField") {
            Debug.Log("2");
            gameObject.SetActive(false);
        }
    }
}
