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
    [Header("ItemPrefab")]
    [SerializeField] private SpriteRenderer back;
    [SerializeField] private SpriteRenderer image;

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
