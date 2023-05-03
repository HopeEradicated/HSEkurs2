using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private float waitTime = 5f;

    public GameObject bulletSample;
    [SerializeField] private bool isFliped;
    [SerializeField] private Animator shooterAnimator;
    [SerializeField] private Transform shootPoint;

    private Coroutine bulletCoroutine; 

    private void Start() {
        isFliped = gameObject.GetComponent<SpriteRenderer>().flipX;
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting() {
        while (true) {
            yield return new WaitForSeconds(waitTime);
            shooterAnimator.Play("ArcherAttack");
        }
    }

    private void CreateBullet() {
        GameObject curBullet = Instantiate(bulletSample, shootPoint.position, Quaternion.identity);
        curBullet.GetComponent<Bullet>().SetIsFliped(!isFliped);
    }
}
