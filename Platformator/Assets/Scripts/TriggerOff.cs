using UnityEngine;

public class TriggerOff : MonoBehaviour
{
    [SerializeField] //<-- не работает почему-то
    private BoxCollider2D col2D;

    private void Start() {
        col2D = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    /*private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            if (gameObject.transform.position.y > other.gameObject.transform.position.y) {
                Debug.Log("True");
                col2D.isTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        //col2D.isTrigger = false;
    }*/
}
