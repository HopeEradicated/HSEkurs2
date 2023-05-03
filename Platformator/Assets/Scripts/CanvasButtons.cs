using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour
{
    public void StartNewGame() {
        SceneManager.LoadScene("Game");
    }

    public void LoadStartedGame() {
        Debug.Log("Нужно сделать загрузку");
    }

    public void ExitTheGame() {
        Debug.Log("Выходить ещё некуда");
    }
}
