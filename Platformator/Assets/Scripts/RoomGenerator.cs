using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGenerator : MonoBehaviour
{
    private static RoomVariants variants;
    private static SpawnObjects spawner;

    [SerializeField] private GameObject topBorderRooom;
    [SerializeField] private GameObject rightBorderRooom;
    [SerializeField] private GameObject leftBorderRooom;
    [SerializeField] private GameObject bottomBorderRooom;

    private int rand;
    private float waitTime = 1f;
    public bool spawned = false;
    private int spawnBorder = 25;
    private static float theFarestRoomCoordsSum = 0;
    private static Vector2 theFarestRoomPos;

    private static int roomCounter = 0;
    private static int spawnPointCounter = 0;


    public Direction direction;
    public enum Direction{
        Top,
        Right,
        Left,
        Bottom,
        None
    }

    private void Awake() {
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
        if (direction != Direction.None) {
            spawnPointCounter++;
        }
    }

    private void Start() {
        theFarestRoomCoordsSum = 0;
    }

    public void Spawn() {
        //Debug.Log("Spawnpoints left " + spawnPointCounter);
        if(!spawned) {
            GameObject newRoom = null;
            if (direction == Direction.Top ) {
                if (roomCounter < spawnBorder) {
                    rand = UnityEngine.Random.Range(0, variants.topRooms.Count);
                    newRoom = Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);
                } else {
                    newRoom = Instantiate(topBorderRooom, transform.position, topBorderRooom.transform.rotation);
                }
            } else if (direction == Direction.Right) {
                if (roomCounter < spawnBorder) {
                    rand = UnityEngine.Random.Range(0, variants.rightRooms.Count);
                    newRoom = Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);
                } else {
                    newRoom = Instantiate(rightBorderRooom, transform.position, rightBorderRooom.transform.rotation);
                }
            } else if (direction == Direction.Left) {
                if (roomCounter < spawnBorder) {
                    rand = UnityEngine.Random.Range(0, variants.leftRooms.Count);
                    newRoom = Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);
                } else {
                    newRoom = Instantiate(leftBorderRooom, transform.position, leftBorderRooom.transform.rotation);
                }
            } else if (direction == Direction.Bottom) {
                if (roomCounter < spawnBorder) {
                    rand = UnityEngine.Random.Range(0, variants.bottomRooms.Count);
                    newRoom = Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);
                } else {
                    newRoom = Instantiate(bottomBorderRooom, transform.position, bottomBorderRooom.transform.rotation);
                }
            }
            if (direction != Direction.None) {
                workWithNewRoom(newRoom);
                roomCounter++;
                spawnPointCounter--;  
            }
            spawned = true;
            if (newRoom != null) {
                float curRoomCoordsSum = newRoom.transform.position.x + newRoom.transform.position.y;
                if (Math.Abs(curRoomCoordsSum) > theFarestRoomCoordsSum) {
                    theFarestRoomCoordsSum = Math.Abs(curRoomCoordsSum);
                    theFarestRoomPos = newRoom.transform.position;
                }
            }
        } else if (spawnPointCounter <= 0) {
            spawner.isRoomGeneratingFinished = true;
            spawner.SetPortalPos(theFarestRoomPos);
        }
    }

    private void workWithNewRoom(GameObject newRoom) {
            newRoom.transform.SetParent(gameObject.transform.parent);
            foreach(Transform child in newRoom.transform) {
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
            spawnPointCounter--;
        }
    }

    public void FindAllNeededScripts() {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnObjects>();
    }

    public void SetCounterToZero() {
        roomCounter = 0;
    }
}
