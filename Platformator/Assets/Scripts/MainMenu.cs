using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [Header("CanvasButtons")]
    [SerializeField] private Button NewGameButton;
    [SerializeField] private Button LoadButton;
    [SerializeField] private Button ExitButton;

    private int levelsToBoss = 5;
    private string pathPS = "Assets/Resources/PlayerStats.txt";
    private string pathGS = "Assets/Resources/GameSP.txt";

    private void Start() {
       NewGameButton.onClick.AddListener(StartNewGame);
       LoadButton.onClick.AddListener(LoadGame);
       ExitButton.onClick.AddListener(ExitGame); 
    }

    private void StartNewGame() {
        File.Delete(pathPS);
        File.Delete(pathGS);
        SceneManager.LoadScene("Game");
    }

    private void LoadGame() {
        int levelsCounter = 0;
        if (File.Exists(pathPS)) {
            if (File.Exists(pathGS))
                using (StreamReader reader = new StreamReader(pathGS))
                    while (!reader.EndOfStream)
                        levelsCounter = int.Parse(reader.ReadLine());
            if (levelsCounter < levelsToBoss)
                SceneManager.LoadScene("Game");
            else
                SceneManager.LoadScene("Boss");
        }
    }

    private void ExitGame() {
        Application.Quit();
    }
}
