using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Sprite[] hearts;
    public Image heartImage;
    public Text buttonText;
    public Text timerText;
    public int timelimit;
    private float time;
    private PlayerBehavior player;
    private Spawner spawner;

    public GameObject InGameMenupanel;
    public GameObject SettingsPanel;
    public GameObject CustomizationPanel;
    public GameObject ExportDataPanel;

    public Text gameStateText;
    public Text heartsText;
    public Text pointsCollectedText;
    public Text timeDurationText;
    public Text obstacleSpawnText;
    public Text sensitivityText;
    private bool valueChanged = false;

    public Image characterImage;
    public Image backgroundImage;

    public Sprite[] characters;
    public Sprite[] backgrounds;

    public GameObject backgroundObject;

    int cindex = 0;
    int bindex = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();

        time = timelimit;
        timerText.text = "Time: " + (int)(time) + "s";
        timeDurationText.text = " Time Duration: " + (int)time + "s";
        obstacleSpawnText.text = " Obstacle Rate: " + spawner.maxSpawns;
        sensitivityText.text = " Player Sensitivity: " + player.speed;
        gameStateText.text = "Game Paused";
        heartsText.text += player.currentHealth;

        characterImage.sprite = characters[0];
        backgroundImage.sprite = backgrounds[0];
    }

    private void Update()
    {
        
        int current = player.currentHealth;
        heartImage.sprite = hearts[current];
        heartsText.text = " Hearts Maintained: " + player.currentHealth;
        pointsCollectedText.text = " Points Collected: " + ScoreManager.currentScore;

        if (!Spawner.pause)
        {
            if (time >= 0)
            {
                time -= Time.deltaTime;
                gameStateText.text = "Game Paused";
            }
            else
            {
                Spawner.pause = true;
                buttonText.text = "Play";
            }
            timerText.text = "Time: " + (int)(time) + "s";
        }

        if (time <= 0)
        {
            InGameMenupanel.SetActive(true);
            gameStateText.text = "Time's Up!";
        }
    }

    public void PausePlay()
    {
        if (time <= 0 && Spawner.pause)
        {
            Reset();
        }
        else if (valueChanged && Spawner.pause)
        {
            Reset();
            valueChanged = false;
        }
        else if(Spawner.pause)
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

    public void Reset()
    {
        player.ResetOrientation();
        time = timelimit;
        Spawner.pause = false;
        ScoreManager.currentScore = 0;
        InGameMenupanel.SetActive(false);
        player.currentHealth = 3;
        buttonText.text = "Pause";
    }

    public void SetTimeLimit(string limit)
    {
        int temp = int.Parse(limit);
        timelimit = temp;
        timerText.text = "Time: " + (int)(timelimit) + "s";
        timeDurationText.text = " Time Duration: " + (int)timelimit + "s";
        valueChanged = true;
    }

    public void SetObstacleSpawnLimit(string limit)
    {
        int temp = int.Parse(limit);
        spawner.maxSpawns = temp;
        obstacleSpawnText.text = " Obstacle Rate: " + spawner.maxSpawns;
        valueChanged = true;
    }

    public void SetSensitivityLimit(string limit)
    {
        float temp = float.Parse(limit);
        player.speed = temp;
        sensitivityText.text = " Player Sensitivity: " + player.speed;
        valueChanged = true;
    }

    public void ActivateSettingsPanel()
    {
        SettingsPanel.SetActive(true);
        CustomizationPanel.SetActive(false);
        ExportDataPanel.SetActive(false);
    }
    public void ActivateCustomizationPanel()
    {
        SettingsPanel.SetActive(false);
        CustomizationPanel.SetActive(true);
        ExportDataPanel.SetActive(false);
    }
    public void ActivateExportDatePanel()
    {
        SettingsPanel.SetActive(false);
        CustomizationPanel.SetActive(false);
        ExportDataPanel.SetActive(true);
    }

    public void ToggleCharacterRight()
    {
        if(cindex < characters.Length-1)
        {
            cindex++;
        }
        else
        {
            cindex = 0;
        }
        characterImage.sprite = characters[cindex];
        for(int i = 0; i < characters.Length; i++)
        {
            if(i == cindex)
            {
                player.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                player.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ToggleCharacterLeft()
    {
        if (cindex > 0)
        {
            cindex--;
        }
        else
        {
            cindex = characters.Length-1;
        }
        characterImage.sprite = characters[cindex];
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == cindex)
            {
                player.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                player.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ToggleBackgroundRight()
    {
        if (bindex < backgrounds.Length - 1)
        {
            bindex++;
        }
        else
        {
            bindex = 0;
        }
        backgroundImage.sprite = backgrounds[bindex];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (i == bindex)
            {
                backgroundObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                backgroundObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ToggleBackgroundLeft()
    {
        if (bindex > 0)
        {
            bindex--;
        }
        else
        {
            bindex = backgrounds.Length - 1;
        }
        backgroundImage.sprite = backgrounds[bindex];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (i == bindex)
            {
                backgroundObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                backgroundObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
