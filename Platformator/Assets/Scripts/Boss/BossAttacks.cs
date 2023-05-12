using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossAttacks : MonoBehaviour
{
    [SerializeField] private GameObject missileSample;
    [SerializeField] private BossInfo bossInfo;
    [SerializeField] private Animator bossAnimator;
    [Header("Sounds")]
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private AudioSource attackAudioSource;
    [SerializeField] private AudioClip rageSound;
    [SerializeField] private AudioClip specialAttackSound;

    private Coroutine attackCoroutine;

    private float missileSpeed = 6f;
    private float healthStep;

    private List<string> attackNames = new List<string>(){"SimpleAttack", "TrippleAttack"};
    private List<string> specialAttackNames = new List<string>(){"GutlingGun", "SideAttack"};
    private List<float> specialAttackTimeSteps = new List<float>(){0.5f,1.5f};
    private int attackIndex = 0;
    private bool isBossHealthLessThanAHalf;

    private void Start() {
        attackCoroutine = StartCoroutine("PlayAttackAnimation");
        healthStep = bossInfo.GetBossPrimaryHealth() / 2;
        //CallAttackNTimes("SideAttack", 10, 1.5f);
    }

    private void Update() {
        if (bossInfo.GetBossHealth() < bossInfo.GetBossPrimaryHealth() - healthStep && !isBossHealthLessThanAHalf) {
            bossAudioSource.clip = rageSound;
            bossAudioSource.Play();
            attackIndex++;
            isBossHealthLessThanAHalf = true;

            InvokeSpecialAttackWithDelay();
        }
    }

    private IEnumerator PlayAttackAnimation() {
        while (true) {
            bossAnimator.Play("BossAttack");
            
            yield return new WaitForSeconds(2f);
        }
    }

    private void MakeAnAttack() {
        Invoke(attackNames[attackIndex], 0f);
    }
 
    private void SimpleAttack() {
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        GameObject newMissile = Instantiate(missileSample, transform.position, Quaternion.identity);
        Rigidbody2D missileRb = newMissile.GetComponent<Rigidbody2D>();
        
        missileRb.velocity = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y).normalized * missileSpeed;
    }

    private void TrippleAttack() {
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
    }

    private void SideAttack() {
        float speedMod = 5f;
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        GameObject newMissile = Instantiate(missileSample, new Vector2(transform.position.x + 25f, playerPos.y), Quaternion.identity);

        Rigidbody2D newMissileRb = newMissile.GetComponent<Rigidbody2D>();
        newMissileRb.velocity = Vector2.left.normalized * missileSpeed*speedMod;
    }

    private void GutlingGun() {
        float randModifier = Random.Range(-6, 6);
        float speedMod = 3f;

        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        GameObject newMissile = Instantiate(missileSample, transform.position, Quaternion.identity);

        Rigidbody2D newMissileRb = newMissile.GetComponent<Rigidbody2D>();
        newMissileRb.velocity = new Vector2(randModifier * playerPos.x - transform.position.x, playerPos.y - transform.position.y).normalized * missileSpeed * speedMod;
        attackAudioSource.Play();
    }

    private void CallAttackNTimes(string attackName, int n, float timeStep) {
        float timeGap = 0f;
        for(int i = 0; i < n;i++) {
            timeGap += timeStep;
            Invoke(attackName, timeGap);
        }
    }

    private void MakeASpecialAttack() {
        StopCoroutine(attackCoroutine);
        int rand = Random.Range(0, specialAttackNames.Count);
        int amountOfRepeats = 10;

        bossAudioSource.clip = specialAttackSound;
        bossAudioSource.Play();

        CallAttackNTimes(specialAttackNames[rand], amountOfRepeats, specialAttackTimeSteps[rand]);
        Invoke("ResumeDefultAttack", amountOfRepeats* specialAttackTimeSteps[rand]);
    }

    private void InvokeSpecialAttackWithDelay() {
        float bonusAttackDelay = 2f;
        Invoke("MakeASpecialAttack", bonusAttackDelay);
    }

    private void ResumeDefultAttack() {
        attackCoroutine = StartCoroutine("PlayAttackAnimation");

        float specialAttackTimeGap = 5f;

        Invoke("InvokeSpecialAttackWithDelay", specialAttackTimeGap);
    }
}
