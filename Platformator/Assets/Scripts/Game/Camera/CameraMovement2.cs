using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private float _leftLimit = -12.8f, _rightLimit = 12.8f;
    private float _bottomLimit = -7f, _topLimit = 2f;

    private void Update() {
       transform.position = new Vector3(
        Mathf.Clamp(playerTransform.position.x, _leftLimit, _rightLimit),
        Mathf.Clamp(playerTransform.position.y + 4, _bottomLimit, _topLimit),
        transform.localPosition.z);
    }

}
