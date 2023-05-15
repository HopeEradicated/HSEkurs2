using UnityEngine;

public class DamageField : MonoBehaviour
{
    private float swordDmg = 2f;

    private void Start() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("Damage Up I") != -1) 
            swordDmg = 3f;    
        if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("Damage Up II") != -1)
            swordDmg = 4f;
        if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("Damage Up III") != -1) 
            swordDmg = 5f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject entity = other.gameObject;
        if (entity.tag == "Boss") {
            entity.GetComponent<BossInfo>().ChangeBossHealth(-1 * swordDmg);
        } else if (entity.tag == "Enemy") {
            entity.GetComponent<EnemyEntity>().ChangeHealthPoints(-1 * swordDmg);
        }
    }

    private void RotateDamageField() {
        transform.Rotate(0, -180, 0);
    }
}
