using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Sprite[] hearts;
    public Image heartImage;
    public GameObject InGameMenupanel;
    public Text buttonText;
    public Text timerText;
    public int timelimit;
    private float time;
    private PlayerBehavior player;
    private Spawner spawner;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        time = timelimit;
        timerText.text = "Time: " + (int)(time) + "s";
    }

    private void Update()
    {
        
        int current = player.currentHealth;
        heartImage.sprite = hearts[current];
        int points = player.playerPoints;
        if (!Spawner.pause)
        {
            time -= Time.deltaTime;
            timerText.text = "Time: " + (int)(time) + "s";
        }
    }

    public void PausePlay()
    {
        if(Spawner.pause)
        {
            Spawner.pause = false;
            InGameMenupanel.SetActive(false);
            buttonText.text = "Pause";
        }
        else
        {
            Spawner.pause = true;
            InGameMenupanel.SetActive(true);
            buttonText.text = "Play";
        }
    }
}
