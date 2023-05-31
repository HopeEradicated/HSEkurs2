using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Warrior : MonoBehaviour
{
    private float warriorSpeed = 0.5f;
    private float playerCheckDist = 1f;
    private bool isWarriorAttacking;
    private bool attackDelayed;
    [SerializeField] private SpriteRenderer unitSprite;
    [SerializeField] private Rigidbody2D warriorRb;
    [SerializeField] private Animator warriorAnimator;
    [SerializeField] private GameObject damageField;

    private enum Status {
        Patrolling, Attacking
    };

    private void Update() {
        Patrolling();
    }

    private void Patrolling() {
        Vector2 raycastVec;
        int modficator = 0; 
        if (transform.rotation.y == 0) {
            modficator = 1;
            raycastVec = Vector2.right;
        } else {
            modficator = -1;
            raycastVec = Vector2.left;
        }
        var raycastForward = Physics2D.Raycast(transform.position, raycastVec * transform.localScale.x, playerCheckDist);
        if (!isWarriorAttacking) {
            warriorRb.velocity = new Vector2(modficator * warriorSpeed, warriorRb.velocity.y);
            warriorAnimator.SetFloat("velocityHorizontal", Mathf.Abs(warriorRb.velocity.x));
        } else if (raycastForward.collider == null) {
            isWarriorAttacking = false;
        }

        if (raycastForward.collider != null && raycastForward.collider.gameObject.tag == "Player" && !attackDelayed) {
            warriorAnimator.Play("ZombieAttack");
            warriorRb.velocity = new Vector2(0, 0);
            isWarriorAttacking = true;
            attackDelayed = true;
            Invoke("DelayAttack", 1.5f);
        }
    }

    private void DelayAttack() {
        if (gameObject != null) {
            attackDelayed = false;
        }
    }

    private void SetDmgFieldActive() {
        damageField.SetActive(true);
    }

    private void HideDmgField() {
        damageField.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            //other.gameObject.GetComponent<Player>().ChangeHealthPoints(-1);
        } else if (other.gameObject.tag == "Ground" || other.gameObject.transform.position.y > gameObject.transform.position.y || other.gameObject.tag == "Border") {
            transform.rotation = Quaternion.Euler(0, Convert.ToInt32((transform.rotation.y == 0)) * 180, 0);
        }

    }
}
