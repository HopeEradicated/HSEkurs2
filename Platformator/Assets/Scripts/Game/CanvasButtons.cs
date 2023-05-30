using UnityEngine;
using UnityEngine.SceneManagement;
public class CanvasButtons : MonoBehaviour
{
    public void ReturnToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
