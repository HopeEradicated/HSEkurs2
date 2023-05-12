using UnityEngine;

public class CheckForTouches : MonoBehaviour
{
    [SerializeField] private BreakThePlatform parentScript;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            parentScript.HandleTheTouch();
        }
    }
}
