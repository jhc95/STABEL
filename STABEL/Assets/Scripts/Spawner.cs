using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public float deltaTime;
    // The deltaTime to pass to spawn new objects
    [SerializeField]
    public int maxSpawns;
    public int numberOfRewards = 2;
    public Vector3 spawnPos;
    public GameObject spawnee;
    public GameObject reward;
    private float currentTime;
    private System.Random rnd = new System.Random();
    private SpriteRenderer sprite; //Declare a SpriteRenderer variable to holds our SpriteRenderer component


    // Use this for initialization
    void Start()
    {
        currentTime = Time.time;
        sprite = GetComponent<SpriteRenderer>(); //Set the reference to our SpriteRenderer component

    }

    // Update is called once per frame
    void Update()
    {
/*        if (Time.time >= currentTime + deltaTime * 7)
        {
                float spawnY = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                float spawnX = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

                Vector2 spawnPosition = new Vector2(spawnX, spawnY);
                GameObject rewardObj = Instantiate(reward, spawnPosition, Quaternion.identity);
                Destroy(rewardObj.gameObject, 2);
        }*/
        // Spawn objects after 1 second passed
        if (Time.time >= currentTime + deltaTime)
        {
            //code something
            int numberOfSpawns = rnd.Next(1, maxSpawns);
            for (int i = 0; i < numberOfSpawns; i++)
            {
                int xPos = rnd.Next(- (int) sprite.bounds.size.x, (int)sprite.bounds.size.x);
                spawnPos = new Vector3(xPos, this.transform.position.y, this.transform.position.z);

                Instantiate(spawnee, spawnPos, new Quaternion());

            }
            currentTime = Time.time;
        }
    }
}
