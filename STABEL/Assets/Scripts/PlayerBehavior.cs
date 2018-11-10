using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public Rigidbody2D rigid;
    public float speed;
    private Vector3 direcInit = Vector3.zero;
    public int currentHealth;
    public int maxHealth = 3;
    public int playerPoints = 0;
    Vector3 PrevPos;
    Vector3 NewPos;
    Vector3 ObjVelocity;
    public static float velocity;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        direcInit.x = Input.acceleration.x;
        direcInit.z = Input.acceleration.z;
        direcInit.y = Input.acceleration.y;
        currentHealth = maxHealth;
        PrevPos = transform.position;
        NewPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Spawner.pause)
        {
            Vector3 dir = Vector3.zero;
            dir.x = Input.acceleration.x - direcInit.x;
            dir.y = Input.acceleration.y - direcInit.y * 0.005f;
            dir.z = Input.acceleration.z - direcInit.z;
            if (dir.sqrMagnitude > 1)
            {
                dir.Normalize();
            }
            dir *= Time.deltaTime;
            transform.Translate(dir * speed);
            if (transform.position.x <= -9f)
            {
                transform.position = new Vector3(-9f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x >= 9f)
            {
                transform.position = new Vector3(9f, transform.position.y, transform.position.z);
            }
            else if (transform.position.y <= -5f)
            {
                transform.position = new Vector3(transform.position.x, -5f, transform.position.z);
            }
            else if (transform.position.y >= 5f)
            {
                transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
            }

            if (currentHealth == 0) {
                Destroy(GameObject.Find("Player"));
                Spawner.pause = true;
                ScoreManager.dead = true;
            }
            NewPos = transform.position;  // each frame track the new position
            velocity = ((NewPos - PrevPos) / Time.fixedDeltaTime).magnitude;  // velocity = dist/time
            PrevPos = NewPos;  // update position for next frame calculation
        }   
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hit(1);
            ScoreManager.hit++;
        }

        if (collision.gameObject.tag == "Reward")
        {
            ScoreManager.currentScore++;
        }
    }

 

    public void Hit (int dmg)
    {
        if(currentHealth > 0)
            currentHealth -= dmg;
    }
}
