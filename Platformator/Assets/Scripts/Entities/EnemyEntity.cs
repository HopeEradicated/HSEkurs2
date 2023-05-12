using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public float health;
    public float value;
    private float riskCoef;
    private GameObject Player;
    [Header("Audio")]
    [SerializeField] private AudioSource bodyAudioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ChangeHealthPoints(float value) {
        health += value;
        if (value < 0) {
            VisualizeDamage();
        }
        if (health <= 0) {
            Player.GetComponent<Player>().stats.money += 1;
            Die();
        }
    }

    private void Die() {
        bodyAudioSource.clip = deathSound;
        bodyAudioSource.Play();
        Destroy(gameObject, 0.4f);
    }

    private void VisualizeDamage() {
        SpriteRenderer entitySR = gameObject.GetComponent<SpriteRenderer>();
        bodyAudioSource.clip = hitSound;
        bodyAudioSource.Play();
        Color entityColor =  entitySR.color;
        entityColor.a = 0.7f;
        entitySR.color = entityColor;
        Invoke("SetDefultOpacity", 0.2f);
    }

    private void SetDefultOpacity() {
        SpriteRenderer entitySR = gameObject.GetComponent<SpriteRenderer>();
        Color entityColor =  entitySR.color;
        entityColor.a = 255;
        entitySR.color = entityColor;
    }
}
