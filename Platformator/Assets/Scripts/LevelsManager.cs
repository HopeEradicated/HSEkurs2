using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private GameObject portalTemplate;

    public void SpawnPortalPos(Vector2 portalPos) {
        //Задаём координату по оси z, чтобы визуально портал отображался поверх других объектов на сцене
        Vector3 curPortalPos = (Vector3)portalPos;
        curPortalPos.z = -5;
        Instantiate(portalTemplate, curPortalPos, portalTemplate.transform.rotation);
    }
}
