using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool toMenu;
    private static int levelsCounter;
    private int levelsToBoss = 5;
    private string path = "Assets/Resources/GameSP.txt";

    [SerializeField] private AudioSource portalAudioSource;

    private void Start() {
        if (File.Exists(path))
            using (StreamReader reader = new StreamReader(path))
                while (!reader.EndOfStream)
                    levelsCounter = int.Parse(reader.ReadLine());
        Debug.Log("Player has completed " + levelsCounter + " levels");
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (toMenu) {
                GameObject Player = GameObject.FindGameObjectWithTag("Player");
                Destroy(Player);
                SceneManager.LoadScene("MainMenu");
            }
            else
                LoadNextLevel();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerField") {
            portalAudioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerField") {
            portalAudioSource.Pause();
        }
    }

    public void LoadNextLevel() {
        levelsCounter++;
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Player>().ChangeLevel(1);
        Player.transform.position = new Vector3(0,0,0);
        Player.GetComponent<SpriteRenderer>().enabled = false;
        Player.GetComponent<PlayerAttack>().enabled = false;  

        if (levelsCounter < levelsToBoss) {
            using (StreamWriter writer = new StreamWriter(path)) {
                writer.WriteLine(levelsCounter);
            }
            SceneManager.LoadScene("PerkSelect");
        } else {
            Destroy(Player);
            SceneManager.LoadScene("Boss");
        }
    }


}
