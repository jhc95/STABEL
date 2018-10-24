using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private PlayerBehavior player;
    public static int currentScore;
    Text score;
    // Use this for initialization
    void Start () {
        currentScore = 0;
        score = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
       
        score.text = "Score: " + currentScore;
	}
}
