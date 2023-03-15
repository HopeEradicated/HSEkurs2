using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    private GameObject generatedLevel; 
    [SerializeField] private GameObject roomHolder;
    [SerializeField] private Player player;

    private void Start() {
        Debug.Log("StartLEveladda");
        generatedLevel = GameObject.FindGameObjectWithTag("Rooms");
    }

    private void Update() {
        if (player.isHealhEqualToZero()) {
            Destroy(roomHolder);
            Instantiate(generatedLevel, new Vector2(0, 0), generatedLevel.transform.rotation);
            player.gameObject.transform.localPosition = Vector3.Lerp(player.gameObject.transform.localPosition, 
            new Vector3(0, 1f,0), 5f);
            for (int i = 0; i < 3; i++) {
                player.ChangeHealthPoints(1);
            }
            roomHolder = generatedLevel;
        }
    }
}
