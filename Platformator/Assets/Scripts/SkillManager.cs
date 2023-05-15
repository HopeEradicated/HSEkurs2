using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SkillArray
{
    public string skillName = "";
    public string skillDescription = "";
    public string SaveToString() {
        return JsonUtility.ToJson(this);
    }
}

public class SkillManager : MonoBehaviour
{
    public List<SkillArray> skillArr = new List<SkillArray>();
    public List<Skill> skillList = new List<Skill>();

    [Header("InteriaPrefab")]
    [SerializeField] private TextMeshProUGUI nameTextBox;
    [SerializeField] private TextMeshProUGUI descTextBox;
    [SerializeField] private TextMeshProUGUI countBox;

    private List<string> selectedSkill = new List<string>();
    private GameObject Player;
    private float delayTimer = 0.0f, delayTime = 0.3f;
    private List<int> Used = new List<int>();
    int curPosition = 0, curSelected = 0, rand = 0;

    private void Start()
    {
        skillList[0].SelectedUpdate();
        Player = GameObject.FindGameObjectWithTag("Player");

        string path = "Assets/Resources/Skills.txt";
        
        StreamReader reader = new StreamReader(path); 
        while(!reader.EndOfStream) {
            SkillArray temp = new SkillArray();
            JsonUtility.FromJsonOverwrite(reader.ReadLine(), temp);
            skillArr.Add(temp);
        }
        reader.Close();

        for(int i=0; i<skillList.Count; i++) {
            do {
                rand = UnityEngine.Random.Range(1,5+1); //skillArr.Count+1);
                if (Used.IndexOf(rand) == -1 && selectedSkill.IndexOf(skillArr[rand].skillName) == -1) break;   
            } while (true);
            Sprite skillSprite = Resources.Load <Sprite> ("Sprites/Skills/Skill" + rand);    
            skillList[i].transform.Find("Image").GetComponent<SpriteRenderer>().sprite = skillSprite;
            skillList[i].skillName = skillArr[rand-1].skillName;
            skillList[i].skillDescription = skillArr[rand-1].skillDescription;
            Used.Add(rand); 
        }
        nameTextBox.text = skillList[curPosition].skillName;
        descTextBox.text = skillList[curPosition].skillDescription;
        countBox.text = curSelected + " / " + Player.GetComponent<Player>().stats.gainedLevel;
    }

    private void Update()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer >= delayTime) {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)) {
                if ((skillList[curPosition].applied && Player.GetComponent<Player>().stats.gainedLevel == curSelected)) {
                    curSelected -= 1;
                    skillList[curPosition].AppliedUpdate();
                }
                else if (Player.GetComponent<Player>().stats.gainedLevel > curSelected ) {
                    if (skillList[curPosition].applied ) 
                        curSelected -= 1;
                    else
                        curSelected += 1;
                    skillList[curPosition].AppliedUpdate();
                }    
                delayTimer = 0.0f;
            }
            if (Input.GetKey(KeyCode.E)) {
                Player.GetComponent<Player>().stats.gainedLevel -= curSelected;
                for(int i=0; i<skillList.Count; i++)
                    if (skillList[i].applied) selectedSkill.Add(skillList[i].skillName);
                Player.GetComponent<Player>().UpdateSkills(selectedSkill);
                Player.GetComponent<Player>().UnloadVar();
                SceneManager.LoadScene("Game");
            }            
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                skillList[curPosition].SelectedUpdate();
                skillList[(curPosition+1)%skillList.Count].SelectedUpdate();
                curPosition = (curPosition+1)%skillList.Count;
                descTextBox.text = skillList[curPosition].skillDescription;
                nameTextBox.text = skillList[curPosition].skillName;
                delayTimer = 0.0f;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                skillList[curPosition].SelectedUpdate();
                curPosition = curPosition == 0 ? skillList.Count-1 : curPosition - 1;
                skillList[curPosition].SelectedUpdate();
                descTextBox.text = skillList[curPosition].skillDescription;
                nameTextBox.text = skillList[curPosition].skillName;
                delayTimer = 0.0f;
            } 
            countBox.text = curSelected + " / " + Player.GetComponent<Player>().stats.gainedLevel;
        }        
    }
}


