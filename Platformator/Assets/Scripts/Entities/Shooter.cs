using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private float waitTime = 5f;

    public GameObject bulletSample;
    private Coroutine attackCoroutine;
    private bool canAttack;
    [SerializeField] private bool isFliped;
    [SerializeField] private Animator shooterAnimator;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioSource weaponAudioSource;

    private Coroutine bulletCoroutine; 

    private void Start() {
        isFliped = gameObject.GetComponent<SpriteRenderer>().flipX;
    }

    private IEnumerator Shooting() {
        while (canAttack) {
            shooterAnimator.Play("ArcherAttack");
            yield return new WaitForSeconds(waitTime);
        }
        StopCoroutine(attackCoroutine);
    }

    private void CreateBullet() {
        GameObject curBullet = Instantiate(bulletSample, shootPoint.position, Quaternion.identity);
        curBullet.GetComponent<Bullet>().SetIsFliped(!isFliped);
        weaponAudioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerField") {
            canAttack = true;
            attackCoroutine = StartCoroutine(Shooting());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerField") {
            canAttack = false;
        }
    }
}
