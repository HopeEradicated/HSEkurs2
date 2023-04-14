using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTheGround : MonoBehaviour
{
    [SerializeField] private PlayerMovements playerMovements;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Missile" && other.gameObject.tag != "RoomShell") {
            playerMovements.canJump = true;
        }
    }
}
