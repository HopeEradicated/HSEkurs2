using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStats
{
    public int level = 0;
    public int money = 0;
    public int expPoints = 0;
    public int expCurMax = 100;
    public int gainedLevel = 0;
    public List<string> selectedSkills = new List<string>();
    public List<string> selectedPerks = new List<string>();

    public PlayerStats() {}
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

public class Player : MonoBehaviour
{
    private string path = "Assets/Resources/PlayerStats.txt";

    public int invuled = 0;
    public bool healed = false;
    public int healthPoints = 3, healthcap = 0;
    public GameObject[] healthIcons;

    public PlayerStats stats = new PlayerStats();

    private bool isPlayerInvulnerable = false, invulnerableSkill = false;

    [Header("Sounds")]
    [SerializeField] private AudioSource bodyAudioSource;
    [SerializeField] private AudioClip lvlupSound;
    [SerializeField] private AudioClip hurtSound;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start() {
        if (File.Exists(path))
            LoadVar();
        healthIcons = GameObject.FindGameObjectsWithTag("Health");
        if (stats.selectedPerks.IndexOf("Strange I") != -1) 
            healthPoints = 4; 
        if (stats.selectedPerks.IndexOf("Strange II") != -1) 
            healthPoints = 5;
        if (stats.selectedPerks.IndexOf("Strange III") != -1) 
            healthPoints = 6;  
        for(int i=0; i<6; i++) {  
            if(i>=healthPoints)
                healthIcons[i].SetActive(false);
            else 
                healthcap++;    
        }
    }
    
    private void Update() {   
        if (Input.GetKeyDown(KeyCode.E) && invuled < 3 && stats.selectedPerks.IndexOf("Inviolability") != -1){
            invulnerableSkill = true;
            VisualizeInvinc();
            Invoke("InvulnerableSkill", 15f);
            invuled++;
            Debug.Log("invuled");
        }
        if (Input.GetKeyDown(KeyCode.F) && !healed && stats.selectedPerks.IndexOf("Heal") != -1){
            ChangeHealthPoints(6);
            healed = true; 
            Debug.Log("healed");
        }
    }

    /*
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open("C:/Unity/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            BinaryFormatter bf = new BinaryFormatter(); 
            FileStream file = File.Create("C:/Unity/MySaveData.dat"); 
            SaveData data = new SaveData()
            bf.Serialize(file, data);
            file.Close();
    */

    public void UnloadVar() {
        string data = stats.SaveToString();
        using (StreamWriter writer = new StreamWriter(path)) {
            writer.WriteLine(data);
        }
        Destroy(gameObject);
    }
    
    public void LoadVar() {
        using (StreamReader reader = new StreamReader(path))
            while(!reader.EndOfStream)
                JsonUtility.FromJsonOverwrite(reader.ReadLine(), stats);
    }

    public void UpdateSkills(List<string> Skills) {
        stats.selectedSkills.AddRange(Skills);
    }

    public void UpdatePerks(List<string> Perks) {
        stats.selectedPerks = Perks;
    }

    public void ChangeHealthPoints(int number) {
        if (number < 0) {
            if(!invulnerableSkill && !isPlayerInvulnerable) {
                for(int i = healthPoints - 1; i >= healthPoints + number; i--)
                    UpdateHealthBar(i, false);
                healthPoints+=number;
                VisualizeDamage();

                bodyAudioSource.clip = hurtSound;
                bodyAudioSource.Play();

                isPlayerInvulnerable = true;
                Invoke("MakePlayerVulnerable", 3f);
            }
        }
        else {
            int i = healthPoints - 1, temp = healthPoints + number;
            if(healthcap < healthPoints + number)
                temp = healthcap;
            if(healthPoints == 0)
                i = healthPoints;
            for(; i < temp; i++)
                UpdateHealthBar(i, true);
            healthPoints+=temp;
        }
    }

    private void MakePlayerVulnerable() {
        isPlayerInvulnerable = false;
    }

    private void InvulnerableSkill() {
        invulnerableSkill = false;
    }

    private void VisualizeInvinc() {
        SpriteRenderer entitySR = gameObject.GetComponent<SpriteRenderer>();
        Color entityColor =  entitySR.color;
        entityColor.a = 0.7f;
        entitySR.color = Color.yellow;
        Invoke("SetDefultOpacity", 15f);
    }

    private void VisualizeDamage() {
        SpriteRenderer entitySR = gameObject.GetComponent<SpriteRenderer>();
        Color entityColor =  entitySR.color;
        entityColor.a = 0.7f;
        entitySR.color = entityColor;
        Invoke("SetDefultOpacity", 0.2f);
    }

    private void SetDefultOpacity() {
        SpriteRenderer entitySR = gameObject.GetComponent<SpriteRenderer>();
        Color entityColor =  entitySR.color;
        entityColor.a = 255;
        entitySR.color = Color.white;
    }

    public void ChangeExperiencePoints(int number) {
        stats.expPoints += number;
        levelUp();
    }

    public void ChangeLevel(int number) {
        stats.level += number; 
        stats.expCurMax = 100 + 100 * stats.level;
        stats.gainedLevel += 1;
    }

    public void levelUp() {
        if (stats.expPoints >= stats.expCurMax) {
            bodyAudioSource.clip = lvlupSound;
            bodyAudioSource.Play();
            stats.level += 1;
            stats.gainedLevel += 1;
            stats.expCurMax = 100 + 100 * stats.level;
        }
    }

    private void UpdateHealthBar(int index, bool isActive) {
        healthIcons[index].SetActive(isActive);
    }

    public bool IsHealhEqualToZero() {
        return (healthPoints == 0);
    }
}
