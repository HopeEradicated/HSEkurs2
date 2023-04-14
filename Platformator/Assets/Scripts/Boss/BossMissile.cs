using UnityEngine;

public class BossMissile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject entity = other.gameObject;
        if (entity.tag == "Player") {
            entity.GetComponent<Player>().ChangeHealthPoints(-1);
        } else if (entity.tag == "Border") {
            Destroy(gameObject);
        }
    }
}
