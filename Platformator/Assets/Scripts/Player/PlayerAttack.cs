using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject missileSample;
    [SerializeField] private GameObject fireBallSample;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject damageField;
    [SerializeField] private AudioSource weaponAudioSource;
    
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
            fireBallSample.transform.localScale = new Vector3(0.9f,0.9f,0);
        }
        else if (Player.GetComponent<Player>().stats.selectedSkills.IndexOf("Smaller is better") != -1 && Player.GetComponent<Player>().stats.selectedSkills.IndexOf("Bigger is better") == -1) {
            missileSpeed = 14f; 
            missileSample.transform.localScale = new Vector3(3f,3f,0);
            fireBallSample.transform.localScale = new Vector3(0.6f,0.6f,0);
        }
        else {
            missileSample.transform.localScale = new Vector3(5f,5f,0);
            fireBallSample.transform.localScale = new Vector3(0.75f,0.75f,0);
        }
    }

    private void Update() {   

<<<<<<< Updated upstream
        if (Input.GetKey(KeyCode.Q) && skillKD && fireballAquered){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            FireBall(mousePos);
            skillKD = false;
=======
        if (Input.GetKey(KeyCode.Q) && skillKD){
            rangeAttackTargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            playerAnimator.Play("Fireball");
>>>>>>> Stashed changes
            Invoke("KD", 10f);
        }
        if (Input.GetButton("Fire1") && canAttack) {
            MeleeAttack();
            canAttack = false;
            Invoke("AttackDelay", 1f);
        }
        if (Input.GetButton("Fire2") && canAttack) {
            rangeAttackTargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            playerAnimator.Play("RangeAttack");
            Invoke("AttackDelay", 1f);
        }
    }

    private void FireBall() {
            GameObject newMissile = Instantiate(fireBallSample, transform.position, Quaternion.identity);
            Rigidbody2D missileRb = newMissile.GetComponent<Rigidbody2D>();
            missileRb.velocity = rangeAttackTargetPos.normalized * (missileSpeed * 0.75f);
            skillKD = false; 
    }

    private void Shoot() {
            GameObject newMissile = Instantiate(missileSample, transform.position, Quaternion.identity);
            Rigidbody2D missileRb = newMissile.GetComponent<Rigidbody2D>();
            missileRb.velocity = rangeAttackTargetPos.normalized * missileSpeed;
            canAttack = false;
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
