using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    private GameObject generatedLevel; 
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject roomsHolder;

    private Player playerInfo;

    private void Start() {
        playerInfo = playerObject.GetComponent<Player>();
    }

    private void Update() {
        if (playerInfo.IsHealhEqualToZero()) {
            Destroy(roomsHolder);
            roomsHolder = generatedLevel;
            generatedLevel.SetActive(true);
            CreateLevelClone();
            playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, new Vector3(0, 1f,0), 5f);
            playerInfo.invuled = 0;
            playerInfo.healed = false;
            playerInfo.ChangeHealthPoints(6);
        }
    }

    public void SetGeneratedLevel() {
        GameObject generatedLevelTemplate = GameObject.FindGameObjectWithTag("Rooms");
        generatedLevel = Instantiate(generatedLevelTemplate, new Vector2(0, 0), Quaternion.identity);
        generatedLevel.SetActive(false);
    }

    private void CreateLevelClone() {
        generatedLevel = Instantiate(generatedLevel, new Vector2(0, 0), Quaternion.identity);
        generatedLevel.SetActive(false);
    }
}
