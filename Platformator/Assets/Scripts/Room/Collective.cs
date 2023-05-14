using UnityEngine;
using System;

public class Collective : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "CollectiveElem" && gameObject.tag == "Player"){
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("More XP") != -1) 
                Player.GetComponent<Player>().ChangeExperiencePoints(10);
            Player.GetComponent<Player>().ChangeExperiencePoints(10);
            Destroy(other.gameObject);
        }
    }
}
