using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Perk : MonoBehaviour
{
    public Perk Prev = null;
    public Perk Opp = null;
    [Header("PerkPrefab")]
    public string perkName;
    public string perkDescription;
    public int cost;
    public bool perkSelected = false;
    public bool perkAvailable = false;
    public bool perkBlocked = false;
    [Header("InteriaPrefab")]
    public TextMeshProUGUI nameTextBox;
    public TextMeshProUGUI descTextBox;
    public TextMeshProUGUI countBox;
    public Image perkImage;
    public Button perkButton;


    private GameObject Player;
    private SpriteRenderer back, image;

    void Start()
    {
        if (Prev == null && !perkSelected)
            perkAvailable = true;  
        else
            perkAvailable = false; 
        Player = GameObject.FindGameObjectWithTag("Player");
        perkButton.onClick.AddListener(PerkButtonOnClick); 
        image = transform.Find("Image").GetComponent<SpriteRenderer>();
        back = transform.Find("Back").GetComponent<SpriteRenderer>(); 
    }

    void Update() {
        if (Prev != null) {
            if (Prev.perkSelected == true && !perkSelected) {
                if (Opp != null) {
                    if (Opp.perkSelected == false && !perkSelected)
                        perkAvailable = true; 
                    else
                        perkBlocked = true;  
                }
                else
                    perkAvailable = true;  
            }
        }
        if(!perkSelected && perkBlocked) {
            back.color = Color.red;
        }
        else if(!perkSelected && perkAvailable) {
            back.color = Color.yellow;
        }
        else if(perkSelected && !perkAvailable) {
            back.color = Color.green;
        }
        else if(!perkSelected && !perkAvailable) {
            back.color = Color.black;          
        }
    }

    void PerkButtonOnClick()
    {
        nameTextBox.text = perkName;
        descTextBox.text = perkDescription;
        perkImage.sprite = image.sprite;
        Debug.Log("Clicked");
        if(cost == 0) 
            countBox.text = "FREE";
        else
            countBox.text = cost + " / " + Player.GetComponent<Player>().stats.money;
    }
}
