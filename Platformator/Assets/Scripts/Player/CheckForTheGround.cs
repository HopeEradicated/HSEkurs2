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

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Border" ) {
            playerMovements.stepSound = other.gameObject.GetComponent<Ground>().stepSound;
            playerMovements.isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Border" ) {
            playerMovements.isGrounded = false;
        }
    }
}
