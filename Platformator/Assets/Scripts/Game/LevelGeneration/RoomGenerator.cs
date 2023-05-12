using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGenerator : MonoBehaviour
{
    private static RoomVariants variants;
    private static SpawnLevel spawnPointsHolder; 
    private static SpawnObjects objectsSpawner;
    private static GameObject roomsParent;

    [Header("BorderRoomsPrefabs")]
    [SerializeField] private GameObject topBorderRooom;
    [SerializeField] private GameObject rightBorderRooom;
    [SerializeField] private GameObject leftBorderRooom;
    [SerializeField] private GameObject bottomBorderRooom;
    [Header("TreasureRoomsPrefabs")]
    [SerializeField] private GameObject rightTreasureRooom;
    [SerializeField] private GameObject leftTreasureRooom;
 
    private int rand;
    public bool spawned = false;
    private int spawnBorder = 25;
    private static float theFarestRoomCoordsSum = 0;

    private static int roomCounter = 0;


    public Direction direction;
    public enum Direction{
        Top,
        Right,
        Left,
        Bottom,
        None
    }

    private void Awake() {
        if (direction != Direction.None) {
            spawnPointsHolder.spawnPoints.Add(gameObject);
        }
    }

    private void Start() {
        theFarestRoomCoordsSum = 0;
    }

    public void Spawn() {
        if(!spawned) {
            GameObject newRoom = null;
            if (direction == Direction.Top ) {
                if (roomCounter < spawnBorder) {
                    rand = UnityEngine.Random.Range(0, variants.topRooms.Count);
                    newRoom = Instantiate(variants.topRooms[rand], transform.position, Quaternion.identity);
                } else {
                    newRoom = Instantiate(topBorderRooom, transform.position, Quaternion.identity);
                }
            } else if (direction == Direction.Right) {
                if (roomCounter < spawnBorder) {
                    rand = UnityEngine.Random.Range(0, variants.rightRooms.Count);
                    newRoom = Instantiate(variants.rightRooms[rand], transform.position, Quaternion.identity);
                } else {
                    rand = UnityEngine.Random.Range(0, 100);
                    if (rand > 10) {
                        newRoom = Instantiate(rightBorderRooom, transform.position, Quaternion.identity);
                    } else {
                        newRoom = Instantiate(rightTreasureRooom, transform.position, Quaternion.identity);
                    }
                }
            } else if (direction == Direction.Left) {
                if (roomCounter < spawnBorder) {
                    rand = UnityEngine.Random.Range(0, variants.leftRooms.Count);
                    newRoom = Instantiate(variants.leftRooms[rand], transform.position, Quaternion.identity);
                } else {
                    rand = UnityEngine.Random.Range(0, 100);
                    if (rand > 10) {
                        newRoom = Instantiate(leftBorderRooom, transform.position, Quaternion.identity);
                    } else {
                        newRoom = Instantiate(leftTreasureRooom, transform.position, Quaternion.identity);
                    }
                }
            } else if (direction == Direction.Bottom) {
                if (roomCounter < spawnBorder) {
                    rand = UnityEngine.Random.Range(0, variants.bottomRooms.Count);
                    newRoom = Instantiate(variants.bottomRooms[rand], transform.position, Quaternion.identity);
                } else {
                    newRoom = Instantiate(bottomBorderRooom, transform.position, Quaternion.identity);
                }
            }
            if (direction != Direction.None) {
                workWithNewRoom(newRoom);
                roomCounter++;
            }
            spawned = true;
            if (newRoom != null) {
                float curRoomCoordsSum = newRoom.transform.position.x + newRoom.transform.position.y;
                if (Math.Abs(curRoomCoordsSum) > theFarestRoomCoordsSum) {
                    theFarestRoomCoordsSum = Math.Abs(curRoomCoordsSum);
                    objectsSpawner.SetPortalPos(newRoom.transform.position);
                }
            }
        }
    }

    private void workWithNewRoom(GameObject newRoom) {
            newRoom.transform.SetParent(roomsParent.transform);
            foreach(Transform child in newRoom.transform) {
                if (child.gameObject.tag == "EnemySpawnPoint") {
                    objectsSpawner.enemySpawnPoints.Add(child.gameObject);
                } else if (child.gameObject.tag == "CollectableSpawnPoint") {
                    objectsSpawner.collectableSpawnPoints.Add(child.gameObject);
                }
            }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Direction thisPointDir = gameObject.GetComponent<RoomGenerator>().direction;
        if (other.CompareTag("RoomPoint")) {
            Direction otherPointDir = other.gameObject.GetComponent<RoomGenerator>().direction;
            if (otherPointDir != Direction.None && thisPointDir == Direction.None) {
                Destroy(other.gameObject);
            } 
            
            if (thisPointDir == Direction.None && other.gameObject.tag == "LowerPrioritySpawnPoint") {
                Debug.Log("Collision");

                Destroy(other.gameObject.transform.parent.gameObject);
                roomCounter--;
            }
        }
    }

    public void FindAllNeededScripts() {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        spawnPointsHolder = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnLevel>();
        objectsSpawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnObjects>();
        roomsParent = GameObject.FindGameObjectWithTag("Rooms");
    }

    public void SetCounterToZero() {
        roomCounter = 0;
    }
}
