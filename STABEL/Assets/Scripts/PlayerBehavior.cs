﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    private Rigidbody2D rigid;
    float speed = 40f;
    private Vector3 direcInit = Vector3.zero;
    public int currentHealth;
    public int maxHealth = 3;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        direcInit.x = Input.acceleration.x;
        direcInit.z = Input.acceleration.z;
        direcInit.y = Input.acceleration.y;
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x - direcInit.x;
        dir.y = Input.acceleration.y - direcInit.y - 0.005f;
        dir.z = Input.acceleration.z - direcInit.z;
        if (dir.sqrMagnitude > 1) {
            dir.Normalize();
        }
        dir *= Time.deltaTime;
        transform.Translate(dir * speed);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hit(1);
        }
    }
    public void Hit (int dmg)
    {
        currentHealth -= dmg;
    }
}