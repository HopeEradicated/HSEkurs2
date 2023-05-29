using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{    
    [Header("Skill Icons")]
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject invul;
    [SerializeField] private GameObject heal;

    private void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("Fireball") != -1) 
            fireBall.SetActive(true);
        if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("Inviolability") != -1) 
            invul.SetActive(true);
        if (Player.GetComponent<Player>().stats.selectedPerks.IndexOf("Heal") != -1) 
            heal.SetActive(true);
    }
}