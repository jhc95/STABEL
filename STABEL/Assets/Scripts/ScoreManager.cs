using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private PlayerBehavior player;
    public static int counter;
    public static float velCounter;
    public static int hit;
    public static int currentScore;
    public static float max;
    public static float distCounter;
    public static float maxDist;
    public static float yMaxTilt;
    public static float xMaxTilt;

    Text score;


    // Use this for initialization
    void Start () {
        currentScore = 0;
        counter = 0;
        velCounter = 0f;
        hit = 0;
        max = 0f;
        score = GetComponent<Text>();
}
	
	// Update is called once per frame
	void Update () {
        score.text = "Score: " + currentScore;
        if (!Spawner.pause)
        {
            counter++;
            float dist = PlayerBehavior.dist;
            float maxVel = PlayerBehavior.velocity;
            float xTilt = PlayerBehavior.xtilt;
            float yTilt = PlayerBehavior.ytilt;

            distCounter = distCounter + dist;
            velCounter = velCounter + maxVel;
            if (max < maxVel)
            {
                max = maxVel;
            }

            if (maxDist < dist)
            {
                maxDist = dist;
            }

            if (xMaxTilt < xTilt)
            {
                xMaxTilt = xTilt;
            }

            if (yMaxTilt < yTilt)
            {
                yMaxTilt = yTilt;
            }
        }
    }
}
