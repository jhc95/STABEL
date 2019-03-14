using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDShifter : MonoBehaviour
{
    public bool allowedToMove;
    public Vector2 vel;
    private double hBound;
    private double vBound;

    // Start is called before the first frame update
    void Start()
    {
        hBound = 3.75;
        vBound = 1.52;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowedToMove)
        {
            if(transform.position.x >= hBound || transform.position.x <= -hBound)
            {
                vel.x *= -1;
            }
            if(transform.position.y >= vBound || transform.position.y <= -vBound)
            {
                vel.y *= -1;
            }
            transform.Translate(vel.x, vel.y, 0);
        }
    }
}
