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
    public Button Restart;
    public Button Save;
    public Button Play;
    public Text SavedText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();

        time = timelimit;
        timerText.text = "Time: " + (int)(time) + "s";
        Restart.gameObject.SetActive(true);
        Save.gameObject.SetActive(false);
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

        if (ScoreManager.dead)
        {
            Restart.gameObject.SetActive(true);
            InGameMenupanel.SetActive(true);
            Save.gameObject.SetActive(true);
            Play.gameObject.SetActive(false);
        }
    }

    public void PausePlay()
    {
        if(Spawner.pause)
        {
            Spawner.pause = false;
            InGameMenupanel.SetActive(false);
            buttonText.text = "Pause";
            Save.gameObject.SetActive(false);
            Restart.gameObject.SetActive(false);
            SavedText.gameObject.SetActive(false);
        }
        else
        {
            Spawner.pause = true;
            InGameMenupanel.SetActive(true);
            buttonText.text = "Play";
            Save.gameObject.SetActive(true);
            Restart.gameObject.SetActive(true);
        }
    }


}
