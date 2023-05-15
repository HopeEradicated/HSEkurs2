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
    private SpriteRenderer back, image;

    private void Start() {
        image = transform.Find("Image").GetComponent<SpriteRenderer>();
        back = transform.Find("Back").GetComponent<SpriteRenderer>();
    }

    private void Update() {
        Color c = image.color;
        if(selected && applied) {
            back.color = Color.green;
            c.a = 1;
        }
        if(!selected && applied) {
            back.color = Color.green;
            c.a = 0.5f;
        }
        if(selected && !applied) {
            back.color = Color.yellow;
            c.a = 1;
        }
        if(!selected && !applied) {
            back.color = Color.black;
            c.a = 0.5f;           
        }
        image.color = c;
    }

    public void SelectedUpdate() {
        selected = !selected;
    }

    public void AppliedUpdate() {
        applied = !applied;
    }
}
