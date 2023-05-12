using UnityEngine;
using System;

public class PlatformBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Entity")) {
            GameObject obj = other.gameObject;
            obj.transform.rotation = Quaternion.Euler(0, (Convert.ToInt32(obj.transform.rotation.y == 0)) * 180, 0);
        }
    }
}
