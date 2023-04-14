using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStats
{
    public int level = 0;
    public int expPoints = 0;
    public int expCurMax = 100;
    public int gainedLevel = 0;
    public List<string> selectedSkills = new List<string>();
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

public class Player : MonoBehaviour
{
    private GameObject[] OldPlayer;
    private int healthPoints = 3;
    private int level = 0;
    private int expPoints = 0;
    private int expCurMax = 100;
    public int gainedLevel = 0;
    public List<string> selectedSkills = new List<string>();
    private string path = "Assets/Resources/PlayerStats.txt";
    private GameObject[] healthIcons;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start() {
        healthIcons = GameObject.FindGameObjectsWithTag("Health");
        LoadVar();
        //OldPlayer = GameObject.FindGameObjectsWithTag("Player");
        //if (OldPlayer.Length > 1)
            //if (OldPlayer[0] != null) {
                //OldPlayer[0].GetComponent<Player>().UnloadVar();
                //LoadVar();
            //}
    }

    public void UnloadVar() {
        StreamWriter writer = new StreamWriter(path);
        PlayerStats temp = new PlayerStats();
        temp.level = level;
        temp.expPoints = expPoints;
        temp.expCurMax = expCurMax;
        temp.gainedLevel = gainedLevel;
        temp.selectedSkills = selectedSkills;
        writer.WriteLine(temp.SaveToString());
        writer.Close();
        Destroy(gameObject);
    }

    public void LoadVar() {
        StreamReader reader = new StreamReader(path); 
        while(!reader.EndOfStream) {
            PlayerStats temp = new PlayerStats();
            JsonUtility.FromJsonOverwrite(reader.ReadLine(), temp);
            level = temp.level;
            expPoints = temp.expPoints;
            expCurMax = temp.expCurMax;
            gainedLevel = temp.gainedLevel;
            selectedSkills = temp.selectedSkills ;
        }
        reader.Close();
    }

    public void UpdateSkills(List<string> Skills) {
        for(int i=0; i<Skills.Count; i++)
            selectedSkills.Add(Skills[i]);
    }


    public void ChangeHealthPoints(int number) {
        if (healthPoints + number >= 0 && healthPoints + number <= 3) {
            UpdateHealthBar(healthPoints + (number - 1 * Mathf.Abs(number)) / 2, (number > 0));
            healthPoints += number;
        }
    }

    public void ChangeExperiencePoints(int number) {
        expPoints += number;
        levelUp();
    }

    public void ChangeLevel(int number) {
        level += number;
        expCurMax = 100 + 100 * level;
        gainedLevel += 1;
    }

    public void levelUp() {
        if (expPoints == expCurMax) {
            level += 1;
            gainedLevel += 1;
            expCurMax = 100 + 100 * level;
        }
    }

    private void UpdateHealthBar(int index, bool isActive) {
        healthIcons[index].SetActive(isActive);
    }

    public bool isHealhEqualToZero() {
        return (healthPoints == 0);
    }
}
