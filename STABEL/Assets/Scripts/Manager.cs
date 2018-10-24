using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Sprite[] hearts;
    public Image heartImage;
    private PlayerBehavior player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }

    private void Update()
    {
        int current = player.currentHealth;
        heartImage.sprite = hearts[current];
        int points = player.playerPoints;
    }
}
