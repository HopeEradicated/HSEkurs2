using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private RoomVariants variants;
    private SpawnObjects spawner;

    private int rand;
    private float waitTime = 1f;
    public bool spawned = false;
    private int spawnBorder = 15;

    static int counter = 0;

    public Direction direction;
    public enum Direction{
        Top,
        Right,
        Left,
        Bottom,
        None
    }

   private void Awake() {
    variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnObjects>();
    Destroy(gameObject, waitTime);
    Invoke("Spawn", 0.2f);
   }

   public void Spawn() {
    if(!spawned && counter < spawnBorder) {
        if (direction == Direction.Top ){
            rand = Random.Range(0, variants.topRooms.Count);
            GameObject newRoom = Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);
            workWithNewRoom(newRoom);
            counter++;
        } else if (direction == Direction.Right) {
            rand = Random.Range(0, variants.rightRooms.Count);
            GameObject newRoom = Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);
            workWithNewRoom(newRoom);
            counter++;
        } else if (direction == Direction.Left) {
            rand = Random.Range(0, variants.leftRooms.Count);
            GameObject newRoom = Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);
            workWithNewRoom(newRoom);
            counter++;
        } else if (direction == Direction.Bottom) {
            rand = Random.Range(0, variants.bottomRooms.Count);
            GameObject newRoom = Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);
            workWithNewRoom(newRoom);
            counter++;
        }
        spawned = true;
    } else if (counter >= spawnBorder) {
        spawner.isRoomGeneratingFinished = true;
    }
   }

   private void workWithNewRoom(GameObject newRoom) {
        newRoom.transform.SetParent(gameObject.transform.parent);
        foreach(Transform child in newRoom.transform) {
            //Debug.Log(child.gameObject.tag);
            if (child.gameObject.tag == "EnemySpawnPoint") {
                spawner.enemySpawnPoints.Add(child.gameObject);
            } else if (child.gameObject.tag == "CollectableSpawnPoint") {
                spawner.collectableSpawnPoints.Add(child.gameObject);
            }
        }
   }

   private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("RoomPoint") && gameObject.GetComponent<RoomGenerator>().spawned) {
        //Если комната уже существует, то, если появляется ещё однеа точка спавна, то её нужно удалить 
        if (other.GetComponent<RoomGenerator>().direction != Direction.None) {
            Destroy(other.gameObject);
        //Почему-то некоторые комнаты всё равно накладываются друг на друга, поэтому в такой ситуации мы просто удаляем ту комнату, которая появилась позже
        } else if (gameObject.GetComponent<RoomGenerator>().direction == Direction.None) {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
   }
}
