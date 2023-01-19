using UnityEngine;

public class TriggerOff : MonoBehaviour
{
    private BoxCollider2D col2D;

    //Получаем collider отдельной платформы
    private void Start() {
        col2D = GetComponent<BoxCollider2D>();
    }

    //Если объект падает на платформу, то она должна становиться "реальной", для этого утверждение isTrigger делаем ложным
    private void OnTriggerEnter2D(Collider2D other) {
        if (transform.position.y < other.gameObject.transform.localPosition.y) {
            col2D.isTrigger = false;
        }
    }

    //Если объект больше не соприкосается, с платформой, то она обратно становится триггером
    private void OnCollisionExit2D(Collision2D other) {
        col2D.isTrigger = true;
    }

    //Потенциально закрывает слабое место программы, к примеру, если на платформе находится другой физический объект, то он должен не упасть с неё, если игрок спрыгнет с платформы, но это не точно 
    private void OnCollisionStay2D(Collision2D other) {
        col2D.isTrigger = false;
    }

}
