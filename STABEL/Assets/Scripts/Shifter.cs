using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shifter : MonoBehaviour
{
    public bool allowedToMove;
    public float vel;
    private double bound;

    // Start is called before the first frame update
    void Start()
    {
        bound = 13.73;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowedToMove)
        {
            if (transform.position.x >= bound || transform.position.x <= -bound)
            {
                vel *= -1;
            }
            transform.Translate(vel, 0, 0);
        }
    }
}
