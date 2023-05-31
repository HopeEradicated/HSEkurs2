using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [Header("CanvasButtons")]
    [SerializeField] private Button TrainingButton;
    [SerializeField] private Button NewGameButton;
    [SerializeField] private Button LoadButton;
    [SerializeField] private Button ExitButton;

    private int levelsToBoss = 5;
    private string pathPS = Application.dataPath + "/Resources/PlayerStats.txt";
    private string pathGS = Application.dataPath + "/Resources/GameSP.txt";

    private void Start() {
        TrainingButton.onClick.AddListener(StartTraining);
        NewGameButton.onClick.AddListener(StartNewGame);
        LoadButton.onClick.AddListener(LoadGame);
        ExitButton.onClick.AddListener(ExitGame); 
    }

    private void StartTraining() {
        SceneManager.LoadScene("Training");
    }

    private void StartNewGame() {
        if (File.Exists(pathPS))
            File.Delete(pathPS);
        else
            File.Create(pathPS).Close();

        if (File.Exists(pathGS))
            File.Delete(pathGS);
        else
            File.Create(pathGS).Close();
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
