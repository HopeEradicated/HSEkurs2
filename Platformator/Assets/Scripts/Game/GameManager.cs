using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnObjects objectsSpawner;
    [SerializeField] private RestartLevel restartLevel;
    [SerializeField] private GameObject loadingCanvas;

    private void OnEnable() {
        EventBus.onGenerationFinished += TriggerObjectsSpawn;
        EventBus.onGenerationFinished += SetGeneratedLevel;
        EventBus.onGenerationFinished += HideLoadingScreen;
    }

    private void OnDisable() {
        EventBus.onGenerationFinished -= TriggerObjectsSpawn;
        EventBus.onGenerationFinished -= SetGeneratedLevel;
        EventBus.onGenerationFinished -= HideLoadingScreen;
    }

    private void HideLoadingScreen() {
        EventBus.isLevelGenerated = true;
        loadingCanvas.SetActive(false);
    }

    private void TriggerObjectsSpawn() {
        objectsSpawner.SpawnAllObjects();
    }

    private void SetGeneratedLevel() {
        restartLevel.SetGeneratedLevel();
    }

}
