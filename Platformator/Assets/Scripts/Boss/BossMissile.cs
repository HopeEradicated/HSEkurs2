using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMissile : MonoBehaviour
{
    private bool isMissileInTheRoom;
    private float rotateSpeed = 2f;
    void Update(){
        transform.Rotate(0f, 0f, rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject entity = other.gameObject;
        if (entity.tag == "Player") {
            Player player = entity.GetComponent<Player>();
            //WARNING!! Потенциально плохо написанный код
            player.ChangeHealthPoints(-1);
            if (player.IsHealhEqualToZero()) {
                SceneManager.LoadScene("EndOfTheGame");
            }
        } else if (entity.tag == "Border" ) {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y != 0){
                Destroy(gameObject);
            } else if (!isMissileInTheRoom) {
                isMissileInTheRoom = true;
            } else if (isMissileInTheRoom) {
                Destroy(gameObject);
            }
        }
    }
}
