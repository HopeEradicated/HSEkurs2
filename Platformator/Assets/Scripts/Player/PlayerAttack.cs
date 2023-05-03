using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject missileSample;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject damageField;
    
    public float missileSpeed = 11f;
    private bool canAttack = true; 

    private void Update() {
        if (Input.GetButton("Fire1") && canAttack) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            MeleeAttack();
            canAttack = false;
            Invoke("AttackDelay", 1f);
        }
    }

    /*private void Shoot(Vector2 mousePos) {
            GameObject newMissile = Instantiate(missileSample, transform.position, Quaternion.identity);
            Rigidbody2D missileRb = newMissile.GetComponent<Rigidbody2D>();
            missileRb.velocity = mousePos.normalized * missileSpeed; 
    }*/

    private void MeleeAttack() {
        playerAnimator.Play("Attack");    
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
