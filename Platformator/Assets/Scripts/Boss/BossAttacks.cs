using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossAttacks : MonoBehaviour
{
    [SerializeField] private GameObject missileSample;
    [SerializeField] private BossInfo bossInfo;
    private Coroutine attackCoroutine;

    private float missileSpeed = 6f;
    private float healthStep;

    private List<string> attackNames = new List<string>(){"SimpleAttack", "TrippleAttack"};
    private List<string> specialAttackNames = new List<string>(){"GutlingGun", "SideAttack"};
    private int attackIndex = 0;
    private bool isBossHealthLessThanAHalf;

    private void Start() {
        attackCoroutine = StartCoroutine(attackNames[attackIndex]);
        healthStep = bossInfo.GetBossPrimaryHealth() / 2;
        //CallAttackNTimes("SideAttack", 10, 1.5f);
    }

    private void Update() {
        if (bossInfo.GetBossHealth() < bossInfo.GetBossPrimaryHealth() - healthStep && !isBossHealthLessThanAHalf) {
            StopCoroutine(attackCoroutine);
            attackIndex++;
            attackCoroutine = StartCoroutine(attackNames[attackIndex]);
            isBossHealthLessThanAHalf = true;
        }
    }

    private IEnumerator SimpleAttack() {
        while (true) {
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            GameObject newMissile = Instantiate(missileSample, transform.position, Quaternion.identity);
            Rigidbody2D missileRb = newMissile.GetComponent<Rigidbody2D>();
            
            missileRb.velocity = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y).normalized * missileSpeed;
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator TrippleAttack() {
        while (true) {
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            GameObject firstMissile = Instantiate(missileSample, transform.position, Quaternion.identity);
            GameObject secondMissile = Instantiate(missileSample, transform.position, Quaternion.identity);
            GameObject thirdMissile = Instantiate(missileSample, transform.position, Quaternion.identity);

            Rigidbody2D firstMissileRb = firstMissile.GetComponent<Rigidbody2D>();
            Rigidbody2D secondMissileRb = secondMissile.GetComponent<Rigidbody2D>();
            Rigidbody2D thirdMissileRb = thirdMissile.GetComponent<Rigidbody2D>();

            firstMissileRb.velocity = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y).normalized * missileSpeed;
            secondMissileRb.velocity = new Vector2(1.2f*playerPos.x + 1f - transform.position.x, 0.7f*playerPos.y - transform.position.y).normalized * missileSpeed;
            thirdMissileRb.velocity = new Vector2(0.7f*playerPos.x - 1f - transform.position.x, 1.2f*playerPos.y - transform.position.y).normalized * missileSpeed;
            yield return new WaitForSeconds(2f);
        }
    }

    private void SideAttack() {
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        GameObject newMissile = Instantiate(missileSample, new Vector2(transform.position.x + 25f, playerPos.y), Quaternion.identity);

        Rigidbody2D newMissileRb = newMissile.GetComponent<Rigidbody2D>();
        newMissileRb.velocity = Vector2.left.normalized * missileSpeed*5;
    }

    private void GutlingGun() {
        float randModifier = Random.Range(-6, 6);

        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        GameObject newMissile = Instantiate(missileSample, transform.position, Quaternion.identity);

        Rigidbody2D newMissileRb = newMissile.GetComponent<Rigidbody2D>();
        newMissileRb.velocity = new Vector2(randModifier * playerPos.x - transform.position.x, playerPos.y - transform.position.y).normalized * missileSpeed * 3;
    }

    private void CallAttackNTimes(string attackName, int n, float timeStep) {
        float timeGap = 1f;
        for(int i = 0; i < n;i++) {
            Invoke(attackName, timeGap);
            timeGap += timeStep;
        }
    }
}
