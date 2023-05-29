using UnityEngine;

public class DamageField : MonoBehaviour
{
    private float swordDmg = 2f;

    private void Start() {
        Invoke("LongStart", 0.1f);
    }

    private void LongStart() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.GetComponent<Player>().stats.selectedSkills.IndexOf("Long arms") != -1) {
            GetComponent<Transform>().transform.localScale = new Vector3(-1.5f,1.2f,0);
        }
        else {
            GetComponent<Transform>().transform.localScale = new Vector3(-1f,1f,0);
        }
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
