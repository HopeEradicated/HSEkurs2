using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerCoords;
    public Camera mainCam;
    private float camMoveSpeed = 10f, camMoveSpeed1 = 23f;

    private void Update() {
        //Debug.Log(mainCam.orthographicSize);
        if (playerCoords.localPosition.y > mainCam.orthographicSize + mainCam.transform.localPosition.y) { 
            moveCameraVertical(mainCam.orthographicSize*2);
        } 
        if (playerCoords.localPosition.y <  mainCam.transform.localPosition.y - mainCam.orthographicSize){
            moveCameraVertical(-2*mainCam.orthographicSize);
        }
        if (playerCoords.localPosition.x > 11.5f + mainCam.transform.localPosition.x) { 
            moveCameraHorizontal(23f);
        } 
        if (playerCoords.localPosition.x <  mainCam.transform.localPosition.x - 11.5f){
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

//playerCoords.localPosition.y > mainCam.orthographicSize + mainCam.transform.localPosition.y || playerCoords.localPosition.y <  mainCam.transform.localPosition.y - mainCam.orthographicSize
//Сделать фоллоу камеру или решить, на что операться, чтобы делать условные локации