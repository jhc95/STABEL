using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private PlayerBehavior player;
    public static int counter;
    public static float velCounter;
    public static bool dead = false;
    public static int hit;
    public static int currentScore;
    public static float max;
    Text score;

    // Use this for initialization
    void Start () {
        currentScore = 0;
        score = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        score.text = "Score: " + currentScore;
        if (!Spawner.pause)
        {
            counter++;
            float maxVel = PlayerBehavior.velocity;
            velCounter = velCounter + maxVel;
            velCounter = velCounter + maxVel;
            if (max < maxVel)
            {
                max = maxVel;
            }
        }
    }
}
