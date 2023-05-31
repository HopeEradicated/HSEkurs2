using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalDirection : MonoBehaviour
{
    public float maxTurnSpeed=90;
    public float smoothTime=0.3f; 
    float angle;
    float currentVelocity;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)){
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.P)){
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        GameObject Portal = GameObject.FindGameObjectWithTag("Portal");
        if (Portal != null) {
            Vector3 portalPosition = Portal.GetComponent<Transform>().position;
            Vector3 direction = portalPosition - transform.position;
            float targetAngle = Vector2.SignedAngle(Vector2.right, direction);
            angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocity, smoothTime, maxTurnSpeed);
            transform.eulerAngles = new Vector3(0, 0, angle - 90);
        }
    }
}
