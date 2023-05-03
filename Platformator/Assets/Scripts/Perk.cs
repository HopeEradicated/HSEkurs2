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
    [Header("PerkPrefab")]
    public string perkName;
    public string perkDescription;
    public int cost;
    public bool perkSelected = false;
    public bool perkAvailable = false;
    [Header("InteriaPrefab")]
    public TextMeshProUGUI nameTextBox;
    public TextMeshProUGUI descTextBox;
    public TextMeshProUGUI countBox;
    public Image perkImage;
    public Button perkButton;


    private GameObject Player;
    private SpriteRenderer back;
    private Image image;

    void Start()
    {
        if (Prev == null)
            perkAvailable = true;  
        else
            perkAvailable = false; 
        Player = GameObject.FindGameObjectWithTag("Player");
        perkButton.onClick.AddListener(PerkButtonOnClick); 
        image = transform.Find("Image").GetComponent<Image>();
        back = transform.Find("Back").GetComponent<SpriteRenderer>(); 
    }

    void Update() {
        if (Prev != null) {
            if (Prev.perkSelected == true && !perkSelected)
                perkAvailable = true;  
        }
        if(!perkSelected && perkAvailable) {
            back.color = Color.yellow;
        }
        if(perkSelected && !perkAvailable) {
            back.color = Color.green;
        }
        if(!perkSelected && !perkAvailable) {
            back.color = Color.black;          
        }
    }

    void PerkButtonOnClick()
    {
        nameTextBox.text = perkName;
        descTextBox.text = perkDescription;
        perkImage.sprite = image.sprite;
        countBox.text = cost + " / " + Player.GetComponent<Player>().stats.money;
    }
}
