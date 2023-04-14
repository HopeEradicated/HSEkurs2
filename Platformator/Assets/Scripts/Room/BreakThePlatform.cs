using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakThePlatform : MonoBehaviour
{
    //Заводим счётчик, который будет хранить, сколько раз мы наступили на платформу
    private int touchesCounter = 0;
    private int breakPoint = 4;

    private void OnCollisionEnter2D(Collision2D other) {
        //Если соприкосновение было с игроком, то увеличиваем счётчик
        if (other.gameObject.tag == "Player") {
            touchesCounter++;
        }
    }

    private void Update() {
        //Разрушаем платформу, если счётчик перевалил за установленную отметку
        if (touchesCounter > breakPoint) {
            Destroy(gameObject);
        }
    }
}
