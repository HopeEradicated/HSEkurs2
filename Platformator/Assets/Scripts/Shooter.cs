using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private float waitTime = 5f;

    public GameObject bulletSample;
    [SerializeField]
    private bool isFliped;

    private void Start() {
        isFliped = gameObject.GetComponent<SpriteRenderer>().flipX;
        StartCoroutine(CreateBullet());
    }

    private void Update() {
        
    }

    private IEnumerator CreateBullet() {
        while (true) {
            GameObject curBullet = Instantiate(bulletSample, gameObject.transform.position, gameObject.transform.rotation);
            curBullet.GetComponent<Bullet>().SetIsFliped(isFliped);
            
            yield return new WaitForSeconds(waitTime);
        }
    }
}
