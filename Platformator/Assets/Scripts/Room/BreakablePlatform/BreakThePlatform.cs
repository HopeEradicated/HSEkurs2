using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakThePlatform : MonoBehaviour
{
    [SerializeField] private List<Sprite> platformSprites;
    [SerializeField] private AudioSource platfromAudioSource;
    //Заводим счётчик, который будет хранить, сколько раз мы наступили на платформу
    private int touchesCounter = 0;
    private int breakPoint = 4;
    private bool skillactive = false;

    private void Start() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.GetComponent<Player>().stats.selectedSkills.IndexOf("Solid construction") != -1) {
            breakPoint = 6;   
            skillactive = true;
        } 
    }

    public void HandleTheTouch() {
        //Если соприкосновение было с игроком, то увеличиваем счётчик
        touchesCounter++;
        platfromAudioSource.Play();
        if(skillactive && ((touchesCounter % 2) == 0))
            SetNextSprite();
        if(!skillactive)
            SetNextSprite();
    }

    private void Update() {
        //Разрушаем платформу, если счётчик перевалил за установленную отметку
        if (touchesCounter >= breakPoint) {
            Destroy(gameObject);
        }
    }

    private void SetNextSprite() {
        if (touchesCounter < breakPoint ) {
            if(skillactive)
                gameObject.GetComponent<SpriteRenderer>().sprite = platformSprites[touchesCounter/2];
            else 
                gameObject.GetComponent<SpriteRenderer>().sprite = platformSprites[touchesCounter];
        }
    }
}
