using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private float warriorSpeed = 1f;
    [SerializeField] private SpriteRenderer unitSprite;

    private enum Status {
        Patrolling, Attacking
    };

    private void Update() {
        Patrolling();
    }

    private void Patrolling() {
        int modficator = 0; 
        if (unitSprite.flipX) {
            modficator = 1;
        } else {
            modficator = -1;
        }
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + modficator * warriorSpeed * Time.deltaTime, gameObject.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag != "Ground" || other.gameObject.transform.position.y > gameObject.transform.position.y) {
            unitSprite.flipX = !unitSprite.flipX;
        }
    }
}
