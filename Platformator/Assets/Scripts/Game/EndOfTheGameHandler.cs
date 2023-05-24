using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndOfTheGameHandler : MonoBehaviour
{
    [SerializeField] TMP_Text messageString;

    private void Start() {
        ChangeMessageString();
    }

    private void ChangeMessageString() {
        if (EventBus.isPlayerWon) {
            messageString.text = "You won!";
        } else {
            messageString.text = "You lose!";
        }
    }
}
