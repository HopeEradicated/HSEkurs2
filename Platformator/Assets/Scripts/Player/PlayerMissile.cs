using UnityEngine;

public class PlayerMissile : MonoBehaviour
{   
    private float missileDmg = 1f;

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject entity = other.gameObject;
        if (entity.tag == "Boss") {
            entity.GetComponent<BossInfo>().ChangeBossHealth(-1 * missileDmg);
        } else if (entity.tag == "Enemy") {
            entity.GetComponent<EnemyEntity>().ChangeHealthPoints(-1 * missileDmg);
        } else if (entity.tag == "Ground" || (entity.tag == "Wall") || (entity.tag == "Border")){
            Destroy(gameObject);
        }
    }
}
