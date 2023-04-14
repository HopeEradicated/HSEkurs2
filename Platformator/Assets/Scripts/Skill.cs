using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [Header("SkillPrefab")]
    public string skillName;
    public string skillDescription;
    public bool selected = false;
    public bool applied = false;
    private SpriteRenderer back;
    private Image image;

    private void Start() {
        image = transform.Find("Image").GetComponent<Image>();
        back = transform.Find("Back").GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if(selected && applied) {
            Color c = image.color;
            back.color = Color.green;
            c.a = 1;
            image.color = c; 
        }
        if(!selected && applied) {
            Color c = image.color;
            back.color = Color.green;
            c.a = 0.5f;
            image.color = c; 
        }
        if(selected && !applied) {
            Color c = image.color;
            back.color = Color.yellow;
            c.a = 1;
            image.color = c; 
        }
        if(!selected && !applied) {
            Color c = image.color;
            back.color = Color.black;
            c.a = 0.5f;
            image.color = c; 
        }
    }

    public void SelectedUpdate() {
        selected = !selected;
    }

    public void AppliedUpdate() {
        applied = !applied;
    }
}
