using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public Rigidbody2D rigid;
    public float speed;
    private Vector3 direcInit = Vector3.zero;
    public int currentHealth;
    public int maxHealth = 3;
    Vector3 PrevPos;
    Vector3 NewPos;
    Vector3 ObjVelocity;
    public static float velocity;
    private Vector2 initialPosition;
    private Vector2 initialRotation;
    public static float dist;
    public AudioSource explosion;
    public AudioSource coinCollection;
    Vector3 dir;
    private Quaternion calibrationQuaternion;
    float maxTilt;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation.eulerAngles;
        direcInit.x = 0;
        direcInit.z = 0;
        direcInit.y = 0;
        currentHealth = maxHealth;
        PrevPos = transform.position;
        NewPos = transform.position;
        velocity = 0;
        CalibrateAccelerometer();
        maxTilt = float.MinValue;
    }

    // Used to calibrate the Input.acceleration
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;

        Quaternion rotateQuaternion = Quaternion.FromToRotation(
            new Vector3(0.0f, 0.0f, -1.2f), accelerationSnapshot);

        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Spawner.pause)
        {
            dir.x = Input.acceleration.x - direcInit.x;
            dir.y = Input.acceleration.y - direcInit.y;
            Vector3 acc = Input.acceleration * speed;
            Vector3 newPosition = new Vector3(dir.x, dir.y, 0) * speed; // 5 to reach end of the screen
            Vector3 fixedAcc = calibrationQuaternion * acc;
            fixedAcc.z = 0f;
            fixedAcc.x *= -1f;
            fixedAcc.y *= -1f;
            transform.position = Vector3.Lerp(transform.position, fixedAcc, speed * Time.deltaTime);
            NewPos = transform.position;  // each frame track the new position
            velocity = ((NewPos - PrevPos) / Time.fixedDeltaTime).magnitude;  // velocity = dist/time
            PrevPos = NewPos;  // update position for next frame calculation
            dist = transform.position.magnitude;
            Debug.Log(dist);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hit(1);
            ScoreManager.hit++;
            explosion.Play();
        }

        if (collision.gameObject.tag == "Reward")
        {
            ScoreManager.currentScore++;
            coinCollection.Play();
        }
    }

 

    public void Hit (int dmg)
    {
        if(currentHealth > 0)
            currentHealth -= dmg;
    }

    public void ResetOrientation()
    {
        gameObject.transform.position = initialPosition;
        gameObject.transform.eulerAngles = initialRotation;
    }
}
