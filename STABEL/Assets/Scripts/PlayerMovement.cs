using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rigid;
    float speed = 40f;
    private Vector3 direcInit = Vector3.zero;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        direcInit.x = Input.acceleration.x;
        direcInit.z = Input.acceleration.z;
        direcInit.y = Input.acceleration.y;
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
}
