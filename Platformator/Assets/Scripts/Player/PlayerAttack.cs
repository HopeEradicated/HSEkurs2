using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject missileSample;
    [SerializeField] private GameObject fireBallSample;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject damageField;

    [Header("AudioResources")]
    [SerializeField] private AudioSource weaponAudioSource;
    [SerializeField] private AudioClip meleeAttackSound;
    [SerializeField] private AudioClip rangeAttackSound;
    [SerializeField] private AudioClip fireBallAttackSound;
    
    private float missileSpeed = 10f;
    private bool canAttack = true, skillKD = true, fireballAquered = false; 
    private GameObject Player;
    private Vector2 rangeAttackTargetPos;
    private void Start() {
        Invoke("LongStart", 0.1f);
    }
    
    private void LongStart() {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("Fireball") != -1)
            fireballAquered = true; 
        if (Player.GetComponent<Player>().stats.selectedSkills.IndexOf("Bigger is better") != -1 && Player.GetComponent<Player>().stats.selectedSkills.IndexOf("Smaller is better") == -1) {
            missileSpeed = 6f; 
            missileSample.transform.localScale = new Vector3(7f,7f,0);
            fireBallSample.transform.localScale = new Vector3(11f,11f,0);
        }
        else if (Player.GetComponent<Player>().stats.selectedSkills.IndexOf("Smaller is better") != -1 && Player.GetComponent<Player>().stats.selectedSkills.IndexOf("Bigger is better") == -1) {
            missileSpeed = 14f; 
            missileSample.transform.localScale = new Vector3(3f,3f,0);
            fireBallSample.transform.localScale = new Vector3(9f,9f,0);
        }
        else {
            missileSample.transform.localScale = new Vector3(5f,5f,0);
            fireBallSample.transform.localScale = new Vector3(7f,7f,0);
        }
    }

    private void Update() {   
        if (Input.GetKey(KeyCode.Q) && skillKD && fireballAquered){
            rangeAttackTargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            FireBallAttack();
            Invoke("KD", 10f);
        }
        if (Input.GetButton("Fire1") && canAttack) {
            MeleeAttack();
            Invoke("AttackDelay", 1f);
        }
        if (Input.GetButton("Fire2") && canAttack) {
            rangeAttackTargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            RangeAttack();
            Invoke("AttackDelay", 1f);
        }
    }

    private void FireBall() {
            GameObject newMissile = Instantiate(fireBallSample, transform.position, Quaternion.identity);
            Rigidbody2D missileRb = newMissile.GetComponent<Rigidbody2D>();
            missileRb.velocity = rangeAttackTargetPos.normalized * (missileSpeed * 0.75f);
            weaponAudioSource.clip = fireBallAttackSound;
            weaponAudioSource.Play();   
            skillKD = false; 
    }

    private void Shoot() {
            GameObject newMissile = Instantiate(missileSample, transform.position, Quaternion.identity);
            Rigidbody2D missileRb = newMissile.GetComponent<Rigidbody2D>();
            missileRb.velocity = rangeAttackTargetPos.normalized * missileSpeed;
            weaponAudioSource.clip = rangeAttackSound;
            weaponAudioSource.Play();   
            canAttack = false;
    }

    private void FireBallAttack() {
        playerAnimator.Play("Fireball");
    }

    private void MeleeAttack() {
        playerAnimator.Play("Attack");
        weaponAudioSource.clip = meleeAttackSound;
        weaponAudioSource.Play();
        canAttack = false;  
    }

    private void RangeAttack() {
        playerAnimator.Play("RangeAttack");
    }

    private void KD() {
        skillKD = true;
    }

    private void AttackDelay() {
        canAttack = true;
    }

    private void SetDmgFieldActive() {
        damageField.SetActive(true);
    }

    private void HideDmgField() {
        damageField.SetActive(false); 
    }
}
