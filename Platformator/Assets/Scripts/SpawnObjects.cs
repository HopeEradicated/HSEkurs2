using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    private int enemyCounter = 0, collectableCounter = 0;
    private int enemyRandBorder = 40, collectableRandBorder = 10;
    private int index1, index2;
    private int rand1, rand2;

    public bool isRoomGeneratingFinished = false;
    private float waitTime = 1f;

    public List<GameObject> enemySpawnPoints, collectableSpawnPoints;
    [SerializeField]
    private GameObject collectableSample;
    [SerializeField]
    private List<GameObject> enemySamples;

    private void Start() {
        StartCoroutine(SpawnAllObjects());   
    }

    private IEnumerator SpawnAllObjects() {
        while (!isRoomGeneratingFinished){
            yield return new WaitForSeconds(waitTime);
        }
        SpawmEnemies();
        SpawnCollectable();
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
                //collectableSpawnPoints[index1].transform.SetParent(null); <- Возможно, это надо, потенциально удалить
                Destroy(collectableSpawnPoints[index1]);
            }
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
                    GameObject curEnemy = Instantiate(enemySamples[rand2], enemySpawnPoints[index2].transform.position, enemySpawnPoints[index2].transform.rotation);
                    curEnemy.transform.SetParent(enemySpawnPoints[index2].transform.parent);
                    if (curEnemy.transform.localPosition.x > 0) {
                        curEnemy.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    enemyCounter++;
                }
                //enemySpawnPoints[index2].transform.SetParent(null); <- Возможно, это надо, потенциально удалить
                Destroy(enemySpawnPoints[index2]);
            }
            enemySpawnPoints.RemoveAt(index2);
            index2--;
        }
    }
}
