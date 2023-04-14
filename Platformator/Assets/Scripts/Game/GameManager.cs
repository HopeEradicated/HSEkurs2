using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnObjects objectsSpawner;
    [SerializeField] private RestartLevel restartLevel;

    private void OnEnable() {
        EventBus.onGenerationFinished += TriggerObjectsSpawn;
        EventBus.onGenerationFinished += SetGeneratedLevel;
    }

    private void OnDisable() {
        EventBus.onGenerationFinished -= TriggerObjectsSpawn;
        EventBus.onGenerationFinished -= SetGeneratedLevel;
    }

    private void TriggerObjectsSpawn() {
        objectsSpawner.SpawnAllObjects();
    }

    private void SetGeneratedLevel() {
        restartLevel.SetGeneratedLevel();
    }

}
