using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerial : MonoBehaviour
{
    private int score;
    private customVector3 playerCoords = new customVector3();

    public Transform playerTransform;

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create("C:/Unity/MySaveData.dat"); 
        SaveData data = new SaveData();
        data.score = score;
        data.playerCoords.setValues(playerTransform.localPosition.x, playerTransform.localPosition.y, playerTransform.localPosition.z);
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {
        if (File.Exists("C:/Unity/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open("C:/Unity/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            score = data.score;
            playerCoords.setValues(data.playerCoords.x, data.playerCoords.y, data.playerCoords.z);
            Debug.Log("Score: " + score + "Coords: " + playerCoords.x + " " + playerCoords.y + " " + playerCoords.z);
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
            //Перепесать для подгрузки с отсутствием каких-либо сохранений
    }

    public void ResetData()
    {
    if (File.Exists("C:/Unity/MySaveData.dat"))
    {
        File.Delete("C:/Unity/MySaveData.dat");
        score = 0;
        playerCoords = new customVector3();
        Debug.Log("Data reset complete!");
    }
    else
        Debug.LogError("No save data to delete.");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "CollectiveElem"){
            score++;
            Debug.Log("Element is collected");
            Destroy(other.gameObject);
        }
    }


}

[Serializable]
class SaveData
{
    public int score;
    public customVector3 playerCoords = new customVector3();

}

[Serializable]
class customVector3
{
    public float x;
    public float y;
    public float z;

    public customVector3(){
        x = 0;
        y = 0;
        z = 0;
    }

    public void setValues(float xCoord, float yCoord, float zCoord){
        x = xCoord;
        y = yCoord;
        z = zCoord;
    }
}
