using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakThePlatform : MonoBehaviour
{
    [SerializeField] private List<Sprite> platformSprites;
    //Заводим счётчик, который будет хранить, сколько раз мы наступили на платформу
    private int touchesCounter = 0;
    private int breakPoint = 3;

    private void OnCollisionEnter2D(Collision2D other) {
        //Если соприкосновение было с игроком, то увеличиваем счётчик
        if (other.gameObject.tag == "Player") {
            touchesCounter++;
            SetNextSprite();
        }
    }

    private void Update() {
        //Разрушаем платформу, если счётчик перевалил за установленную отметку
        if (touchesCounter >= breakPoint) {
            Destroy(gameObject);
        }
    }

    private void SetNextSprite() {
        if (touchesCounter < breakPoint) {
            gameObject.GetComponent<SpriteRenderer>().sprite = platformSprites[touchesCounter];
        }
    }
}
