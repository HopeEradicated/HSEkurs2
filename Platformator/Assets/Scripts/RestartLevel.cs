using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    private static GameObject generatedLevel; 
    [SerializeField] private GameObject playerObject;

    private Player playerInfo;
    private static bool isLevelRestarted;

    private void Start() {
        if (isLevelRestarted) {
            Debug.Log("1 " + generatedLevel + "2 " + playerObject);
            Instantiate(generatedLevel, new Vector2(0, 0), Quaternion.identity);
            Instantiate(playerObject, new Vector2(0, 1), Quaternion.identity);
            isLevelRestarted = false;
        }
        playerInfo = playerObject.GetComponent<Player>();
    }

    private void Update() {
        if (playerInfo.isHealhEqualToZero()) {
            isLevelRestarted = true;
            SceneManager.LoadScene("RestartedLevel");
        }
    }

    public void SetGeneratedLevel() {
        generatedLevel = GameObject.FindGameObjectWithTag("Rooms");
    }
}
