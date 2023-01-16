using UnityEngine;

public class TriggerOff : MonoBehaviour
{
    private BoxCollider2D col2D;

    private void Awake() {
        col2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (transform.localPosition.y < other.gameObject.transform.localPosition.y) {
            col2D.isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        //Если объект перепрыгнул платформу, то он должен на что-то приземлиться, если спрыгнул с неё, то она должна обратно стать триггером
        if (transform.localPosition.y < other.gameObject.transform.localPosition.y) {
            col2D.isTrigger = false;
        } else if (transform.localPosition.y > other.gameObject.transform.localPosition.y) {
            col2D.isTrigger = true;
        }
    }
}
