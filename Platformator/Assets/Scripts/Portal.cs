using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private static int levelsCounter;
    private InitLevel levelInitializer;

    private void Start() {
        levelInitializer = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InitLevel>();
        Debug.Log("Player has completed " + levelsCounter + " levels");
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (Input.GetKey(KeyCode.E) && other.gameObject.tag == "Player") {
            RestartLevel();
        }
    }

    public void RestartLevel() {
        levelsCounter++;
        //Загрузка определённого уровня, переделать
        Destroy(gameObject);
        SceneManager.LoadScene("Level2");
        levelInitializer.CreateObjects();
    }

    private void DestroyOldPortal() {
        Invoke("DestroyOldPortal", 1f);
    }
}
