using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PerkManager : MonoBehaviour
{    
    [Header("InteriaPrefab")]
    [SerializeField] private TextMeshProUGUI nameTextBox;
    [SerializeField] private TextMeshProUGUI countBox;
    [SerializeField] private Button buyButton;

    private GameObject Player;
    private Perk tempPerk;
    private GameObject[] perks;
    private List<string> selectedPerks = new List<string>();

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        perks = GameObject.FindGameObjectsWithTag("Perk");
        foreach (GameObject perk in perks) {
            tempPerk = perk.GetComponent<Perk>();
            if(Player.GetComponent<Player>().stats.selectedPerks.IndexOf(tempPerk.perkName) != -1) 
                tempPerk.perkSelected = true;      
        } 
        countBox.text = "5 / " + Player.GetComponent<Player>().stats.money;
        buyButton.onClick.AddListener(BuyButtonOnClick); 
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E)) {
            foreach (GameObject perk in perks) {
                tempPerk = perk.GetComponent<Perk>();
                if(tempPerk.perkSelected) 
                    selectedPerks.Add(tempPerk.perkName); 
            } 
            Player.GetComponent<Player>().UpdatePerks(selectedPerks);
            SceneManager.LoadScene("SkillSelect");
        }            
    }

    private void BuyButtonOnClick()
    {
        foreach (GameObject perk in perks) {
            tempPerk = perk.GetComponent<Perk>();
            if(tempPerk.perkName == nameTextBox.text && tempPerk.perkAvailable && 
            Player.GetComponent<Player>().stats.money >= tempPerk.cost) {
                Player.GetComponent<Player>().stats.money -= tempPerk.cost;
                tempPerk.perkSelected = true;
                tempPerk.perkAvailable = false;
            }
        }
    }
}


