using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private float warriorSpeed = 0.5f;
    [SerializeField] private SpriteRenderer unitSprite;
    [SerializeField] private Rigidbody2D warriorRb;
    [SerializeField] private Animator warriorAnimator;

    private enum Status {
        Patrolling, Attacking
    };

    private void Update() {
        Patrolling();
    }

    private void Patrolling() {
        int modficator = 0; 
        if (!unitSprite.flipX) {
            modficator = 1;
        } else {
            modficator = -1;
        }
        warriorRb.velocity = new Vector2(modficator * warriorSpeed, warriorRb.velocity.y);
        warriorAnimator.SetFloat("velocityHorizontal", Mathf.Abs(warriorRb.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().ChangeHealthPoints(-1);
        } else if (other.gameObject.tag != "Ground" || other.gameObject.transform.position.y > gameObject.transform.position.y) {
            unitSprite.flipX = !unitSprite.flipX;
        }

    }
}
