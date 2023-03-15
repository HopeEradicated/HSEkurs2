using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private float waitTime = 5f;

    public GameObject bulletSample;
    [SerializeField] private bool isFliped;

    private Coroutine bulletCoroutine; 
    private bool isPlayerNear;
    private bool initCoroutine;

    private void Start() {
        isFliped = gameObject.GetComponent<SpriteRenderer>().flipX;
        StartCoroutine(CreateBullet());
    }

    private void Update() {
        
    }

    private IEnumerator CreateBullet() {
        while (true) {
            yield return new WaitForSeconds(waitTime);

            if (isPlayerNear) {
                GameObject curBullet = Instantiate(bulletSample, gameObject.transform.position, gameObject.transform.rotation);
                curBullet.GetComponent<Bullet>().SetIsFliped(isFliped);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerField") {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerField") {
            isPlayerNear = false;
        }
    }
}
