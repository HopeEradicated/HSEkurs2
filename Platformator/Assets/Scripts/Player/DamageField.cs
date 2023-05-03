using UnityEngine;

public class DamageField : MonoBehaviour
{
    private float swordDmg = 2f;
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject entity = other.gameObject;
        if (entity.tag == "Boss") {
            entity.GetComponent<BossInfo>().ChangeBossHealth(-1 * swordDmg);
        } else if (entity.tag == "Enemy") {
            entity.GetComponent<EnemyEntity>().ChangeHealthPoints(-1 * swordDmg);
            //Debug.Log("Did dmg");
        }
    }

    private void RotateDamageField() {
        transform.Rotate(0, -180, 0);
    }
}
