using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    private int enemyCounter = 0, collectableCounter = 0;
    private int enemyRandBorder = 40, collectableRandBorder = 10;
    private int index1, index2;
    private int rand1, rand2;
    private Vector2 portalPosition;

    [Header("ObjectsPrefabs")]
    [SerializeField] private GameObject portalTemplate;
    public List<GameObject> enemySpawnPoints, collectableSpawnPoints;

    [Header("Scripts")]
    [SerializeField] private GameObject collectableSample;
    [SerializeField] private List<GameObject> enemySamples;

    public void SpawnAllObjects() {
        SpawmEnemies();
        SpawnCollectable();
        SpawnPortalPos();
    }

    private void SpawnCollectable() {
        index1 = collectableSpawnPoints.Count - 1;
        while(index1 >= 0 && collectableCounter <= 10) {
            rand1 = Random.Range(0, 100);
            if (collectableSpawnPoints[index1] != null) {
                if (rand1 < collectableRandBorder) {
                    GameObject curElem =  Instantiate(collectableSample, collectableSpawnPoints[index1].transform.position, collectableSpawnPoints[index1].transform.rotation);
                    curElem.transform.SetParent(collectableSpawnPoints[index1].transform.parent);
                    collectableCounter++;
                } else {
                    collectableRandBorder += 5;
                }
            }
            Destroy(collectableSpawnPoints[index1]);

            collectableSpawnPoints.RemoveAt(index1);
            index1--;
        }
    }

    private void SpawmEnemies() {
        index2 = enemySpawnPoints.Count - 1;
        while (index2 >= 0) {
            rand2 = Random.Range(0,100);
            if (enemySpawnPoints[index2] != null) {
                if (rand2 < enemyRandBorder) {
                    rand2 = Random.Range(0, enemySamples.Count);
                    GameObject curEnemy = Instantiate(enemySamples[rand2], enemySpawnPoints[index2].transform.position, Quaternion.identity);
                    curEnemy.transform.SetParent(enemySpawnPoints[index2].transform.parent);
                    if (curEnemy.transform.localPosition.x > 0 && curEnemy.name == "Shooter(Clone)") {
                        curEnemy.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    enemyCounter++;
                }
            }
            Destroy(enemySpawnPoints[index2]);
            enemySpawnPoints.RemoveAt(index2);
            index2--;
        }
    }

    public void SpawnPortalPos() {
        //Задаём координату по оси z, чтобы визуально портал отображался поверх других объектов на сцене
        Vector3 curPortalPos = (Vector3)portalPosition;
        curPortalPos.z = -5;
        Instantiate(portalTemplate, curPortalPos, portalTemplate.transform.rotation);
    }

    public void SetPortalPos(Vector2 portalPos) {
        portalPosition = portalPos;
    }
}
