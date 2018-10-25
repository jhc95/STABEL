using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Sprite[] hearts;
    public Image heartImage;
    public GameObject InGameMenupanel;
    public Text buttonText;
    private PlayerBehavior player;
    private Spawner spawner;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }

    private void Update()
    {
        int current = player.currentHealth;
        heartImage.sprite = hearts[current];
        int points = player.playerPoints;
    }

    public void PausePlay()
    {
        if(player.pause)
        {
            player.pause = false;
            spawner.pause = false;
            InGameMenupanel.SetActive(false);
            buttonText.text = "Pause";
        }
        else
        {
            player.pause = true;
            spawner.pause = true;
            InGameMenupanel.SetActive(true);
            buttonText.text = "Play";
        }
    }
}
