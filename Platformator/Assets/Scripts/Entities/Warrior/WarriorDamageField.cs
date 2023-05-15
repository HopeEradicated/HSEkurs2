using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDamageField : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int sumHit = 1;
            if (other.gameObject.GetComponent<Player>().stats.selectedPerks.IndexOf("Normal mod") != -1) 
                sumHit++;
            if (other.gameObject.GetComponent<Player>().stats.selectedPerks.IndexOf("Hard mod") != -1) 
                sumHit++;
            other.gameObject.GetComponent<Player>().ChangeHealthPoints(-sumHit);
        }
    }
}
