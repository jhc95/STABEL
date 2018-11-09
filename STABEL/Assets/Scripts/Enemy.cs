using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private PlayerBehavior player;
	// Use this for initialization
	void Start () {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        }
        
	}
	
    void OnTriggerEnter2D (Collider2D col) {
        if (col.CompareTag("Player"))
        {
            player.Hit(1);
        }
    }

    void Update()
    {
        if (!Spawner.pause) {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }
}
