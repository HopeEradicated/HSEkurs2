using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private static int levelsCounter;

    private void Start() {
        Debug.Log("Player has completed " + levelsCounter + " levels");
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (Input.GetKey(KeyCode.E) && other.gameObject.tag == "Player") {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel() {
        levelsCounter++;
        //Загрузка определённого уровня, переделать
        Destroy(gameObject);
        SceneManager.LoadScene("Level2");
    }
}
