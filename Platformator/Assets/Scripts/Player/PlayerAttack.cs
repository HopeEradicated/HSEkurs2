using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject missileSample;
    [SerializeField] private GameObject fireBallSample;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject damageField;
    [SerializeField] private AudioSource weaponAudioSource;
    
    private float missileSpeed = 10f;
    private bool canAttack = true, skillKD = true; 
    private GameObject Player;

    private void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {   

        if (Input.GetKey(KeyCode.Q) && skillKD){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            FireBall(mousePos);
            skillKD = false;
            Invoke("KD", 10f);
        }
        if (Input.GetButton("Fire1") && canAttack) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            MeleeAttack();
            canAttack = false;
            Invoke("AttackDelay", 1f);
        }
        if (Input.GetButton("Fire2") && canAttack) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Shoot(mousePos);
            canAttack = false;
            Invoke("AttackDelay", 1f);
        }
    }

    private void FireBall(Vector2 mousePos) {
            GameObject newMissile = Instantiate(fireBallSample, transform.position, Quaternion.identity);
            Rigidbody2D missileRb = newMissile.GetComponent<Rigidbody2D>();
            missileRb.velocity = mousePos.normalized * (missileSpeed * 0.75f); 
    }

    private void Shoot(Vector2 mousePos) {
            GameObject newMissile = Instantiate(missileSample, transform.position, Quaternion.identity);
            Rigidbody2D missileRb = newMissile.GetComponent<Rigidbody2D>();
            missileRb.velocity = mousePos.normalized * missileSpeed; 
    }

    private void MeleeAttack() {
        playerAnimator.Play("Attack");
        weaponAudioSource.Play();   
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
