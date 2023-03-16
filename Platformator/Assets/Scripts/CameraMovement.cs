using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerCoords;
    public Camera mainCam;
    [SerializeField] private List<GameObject> followObjects;
    private float camMoveSpeed = 14f, camMoveSpeed1 = 24.9f, horizontalDistance = 12.45f;

    private void Update() {
        //Проверяем, если игрок вышел за границы видимости камеры, то двигаем её в нужном направлении
        if (playerCoords.localPosition.y > mainCam.orthographicSize + mainCam.transform.localPosition.y) { 
            moveCameraVertical(mainCam.orthographicSize*2);
        } 
        if (playerCoords.localPosition.y <  mainCam.transform.localPosition.y - mainCam.orthographicSize){
            moveCameraVertical(-2*mainCam.orthographicSize);
        }
        if (playerCoords.localPosition.x > horizontalDistance + mainCam.transform.localPosition.x) { 
            moveCameraHorizontal(24.9f);
        } 
        if (playerCoords.localPosition.x <  mainCam.transform.localPosition.x - horizontalDistance){
            moveCameraHorizontal(-24.9f);
        }
    }

    private void moveCameraVertical(float distance) {
        foreach (var elem in followObjects) {
            elem.transform.localPosition = Vector3.MoveTowards(elem.transform.localPosition, 
            new Vector3(elem.transform.localPosition.x, elem.transform.localPosition.y + distance,elem.transform.localPosition.z), camMoveSpeed);
        }
    }

    private void moveCameraHorizontal(float distance) {
        foreach (var elem in followObjects) {
            elem.transform.localPosition = Vector3.MoveTowards(elem.transform.localPosition, 
            new Vector3(elem.transform.localPosition.x + distance, elem.transform.localPosition.y,elem.transform.localPosition.z), camMoveSpeed1);    
        }
    }
}