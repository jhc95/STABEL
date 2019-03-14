using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mono.Data.Sqlite;
using System.Data;
using System.Text;
using System.IO;

public class Manager : MonoBehaviour {

    public Sprite[] hearts;
    public Image heartImage;
    public Text buttonText;
    public Text timerText;
    public int timelimit;
    private float time;
    public PlayerBehavior player;
    public Spawner spawner;

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

    public Sprite[] characters;
    public Sprite[] backgrounds;
    public static String username;
    public GameObject backgroundObject;

    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;

    public GameObject too_much;
    public GameObject usernameE;
    public GameObject usernameNE;
    public float start_time;
    public Boolean create;

    public int cindex = 0;
    public int bindex = 0;
    public DateTime today;

    public Toggle movingToggle;
    public TwoDShifter stars;
    public Shifter cloud1;
    public Shifter cloud2;
    public Shifter cloud3;
    public Shifter cloud4;
    public Shifter cloud5;
    public Shifter cloud6;

    public Slider colorSlider;

    private void Start()
    {
        create = false;
        start_time = Time.time;
        var fileName = "time.txt";
        today = System.DateTime.Today;
        if (!File.Exists(fileName))
        {
            var myFile = File.CreateText(fileName);
            myFile.WriteLine(today);
            create = true;
        }
        else {
            StreamWriter writer = new StreamWriter(fileName, true);
            writer.WriteLine(today);
            writer.Close();
        }
        double total_played = 0;

        if (File.Exists("time.txt")) {
            StreamReader reader = new StreamReader(fileName);
            while (!reader.EndOfStream) {
                string date = reader.ReadLine();
                DateTime parsed_date = DateTime.Parse(date);
                today = System.DateTime.Today;
                if (parsed_date == today) {
                    string time_played = reader.ReadLine();
                    double played = Convert.ToDouble(time_played);
                    total_played = total_played + played;
                    Debug.Log(total_played);
                }
            }
            reader.Close();
        }


        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        //spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        String connectionString;
        if (Application.platform != RuntimePlatform.Android)
        {

            connectionString = Application.dataPath + "/DB.s3db";
        }
        else
        {

            connectionString = Application.persistentDataPath + "/DB.s3db";
        }
        connection = new SqliteConnection("URI=file:" + connectionString);
        connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM User";
            reader = command.ExecuteReader();
            // Final string to print to text file
            StringBuilder sb = new StringBuilder();
            System.Object[] items = new System.Object[reader.FieldCount];
            while (reader.Read())
            {
                reader.GetValues(items);
                foreach (var item in items)
                {
                    sb.Append(item.ToString());
                    username = item.ToString();
                }
            }
            
            connection.Close();

        if (total_played > 7200.00) {
            too_much.SetActive(true);
            StartCoroutine(Terminate());
        }
        else if (username == null)
        {
            usernameNE.SetActive(true);
        }
        else
        {
            usernameE.SetActive(true);
        }

        time = timelimit;
        timerText.text = "Time: " + (int)(time) + "s";
        timeDurationText.text = " Time Duration: " + (int)time + "s";
        obstacleSpawnText.text = " Obstacle Rate: " + spawner.maxSpawns;
        sensitivityText.text = " Player Sensitivity: " + player.speed;
        gameStateText.text = "Game Paused";
        heartsText.text += player.currentHealth;

        characterImage.sprite = characters[0];
    }

    IEnumerator Terminate() {
        yield return new WaitForSeconds(3);
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        StreamWriter writer = new StreamWriter("time.txt", true);
        if (create) {
            writer.WriteLine(today);
        }
        writer.WriteLine(Time.time - start_time);
        writer.Close();
    }

    private void Update()
    {
        heartImage.sprite = hearts[player.currentHealth];
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
        timeDurationText.text = "Time Duration: " + (int)timelimit + "s";
        valueChanged = true;
    }

    public void SetObstacleSpawnLimit(string limit)
    {
        int temp = int.Parse(limit);
        spawner.maxSpawns = temp;
        obstacleSpawnText.text = "Obstacle Rate: " + spawner.maxSpawns;
        valueChanged = true;
    }

    public void SetSensitivityLimit(string limit)
    {
        float temp = float.Parse(limit);
        player.speed = temp;
        sensitivityText.text = "Player Sensitivity: " + player.speed;
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
        setColorCharacter();
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
        setColorCharacter();
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
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (i == bindex)
            {
                backgroundObject.transform.GetChild(i).gameObject.SetActive(true);
                ToggleBackgroundMoving();
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
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (i == bindex)
            {
                backgroundObject.transform.GetChild(i).gameObject.SetActive(true);
                ToggleBackgroundMoving();
            }
            else
            {
                backgroundObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ToggleBackgroundMoving()
    {
        if (movingToggle.isOn)
        {
            if (bindex == 0)
            {
                stars.allowedToMove = true;
                cloud1.allowedToMove = false;
                cloud2.allowedToMove = false;
                cloud3.allowedToMove = false;
                cloud4.allowedToMove = false;
                cloud5.allowedToMove = false;
                cloud6.allowedToMove = false;
            }
            else if (bindex == 1)
            {
                cloud1.allowedToMove = true;
                cloud2.allowedToMove = true;
                cloud3.allowedToMove = true;
                cloud4.allowedToMove = true;
                cloud5.allowedToMove = true;
                cloud6.allowedToMove = true;
                stars.allowedToMove = false;
            }
        }
        else
        {
            stars.allowedToMove = false;
            cloud1.allowedToMove = false;
            cloud2.allowedToMove = false;
            cloud3.allowedToMove = false;
            cloud4.allowedToMove = false;
            cloud5.allowedToMove = false;
            cloud6.allowedToMove = false;
        }
    }

    public void setColorCharacter()
    {
        Color[] colorData = characterImage.sprite.texture.GetPixels();
        for (int i = 0; i < colorData.Length; i++)
        {
            if(colorData[i].a != 0)
            {
                float H;
                float S;
                float V;
                Color.RGBToHSV(colorData[i], out H, out S, out V);
                colorData[i] = Color.HSVToRGB(colorSlider.value, S, V);
            }
        }
        characterImage.sprite.texture.SetPixels(colorData);
        characterImage.sprite.texture.Apply();
    }
}
