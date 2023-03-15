using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLevel : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToInit;
    [SerializeField] private RoomGenerator roomGenerator;
    [SerializeField] private Transform spawnPointParentTransform;

    private void Awake() {
        roomGenerator.SetCounterToZero();
        roomGenerator.FindAllNeededScripts();
        CreateObjects();
    }

    //На текущий момент эта функция создаёт только точки спавна комнат, но название не меняется намеренно, если в будущем понадобиться создавать ещё что-то 
    public void CreateObjects() {
        foreach(var elem in objectsToInit) {
            GameObject curElem = Instantiate(elem, elem.transform.position, elem.transform.rotation);
            if (elem.tag == "RoomPoint") {
                RoomGenerator tempRoomGenerator = elem.GetComponent<RoomGenerator>();
                tempRoomGenerator.spawned = false;
                curElem.transform.SetParent(spawnPointParentTransform, false);
            }
        }
    }
}
