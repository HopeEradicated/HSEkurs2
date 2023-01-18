using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOn : MonoBehaviour
{
    //Скрипт помогает сделать дизайн комнат более пластичным за счёт реализации механики запрыгивания на платформу снизу
    private GameObject[] triggerPlatforms;
    private GameObject player;

    private void Start() {
        //Находим и получаем игрока и нужные платформы по их тегам
        player = GameObject.FindGameObjectWithTag("Player");
        triggerPlatforms = GameObject.FindGameObjectsWithTag("triggerPlatform");
    }

    private void Update() {
        //Если игрок упал ниже платформы, то она опять должна стать триггером, прогоняем это для всех платформ
        foreach(var elem in triggerPlatforms){
            if (elem.transform.position.y > player.transform.position.y){
                BoxCollider2D tempCol = elem.GetComponent<BoxCollider2D>();
                tempCol.isTrigger = true;
            } 
        }
    }
}
