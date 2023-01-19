using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerCoords;
    public Camera mainCam;
    private float camMoveSpeed = 10f, camMoveSpeed1 = 23f, horizontalDistance = 11.5f;

    private void Update() {
        //Проверяем, если игрок вышел за границы видимости камеры, то двигаем её в нужном направлении
        if (playerCoords.localPosition.y > mainCam.orthographicSize + mainCam.transform.localPosition.y) { 
            moveCameraVertical(mainCam.orthographicSize*2);
        } 
        if (playerCoords.localPosition.y <  mainCam.transform.localPosition.y - mainCam.orthographicSize){
            moveCameraVertical(-2*mainCam.orthographicSize);
        }
        if (playerCoords.localPosition.x > horizontalDistance + mainCam.transform.localPosition.x) { 
            moveCameraHorizontal(23f);
        } 
        if (playerCoords.localPosition.x <  mainCam.transform.localPosition.x - horizontalDistance){
            moveCameraHorizontal(-23f);
        }
    }

    private void moveCameraVertical(float distance){
        mainCam.transform.localPosition = Vector3.MoveTowards(mainCam.transform.localPosition, 
        new Vector3(mainCam.transform.localPosition.x, mainCam.transform.localPosition.y + distance,mainCam.transform.localPosition.z), camMoveSpeed);
    }

    private void moveCameraHorizontal(float distance){
        mainCam.transform.localPosition = Vector3.MoveTowards(mainCam.transform.localPosition, 
        new Vector3(mainCam.transform.localPosition.x + distance, mainCam.transform.localPosition.y,mainCam.transform.localPosition.z), camMoveSpeed1);
    }
}