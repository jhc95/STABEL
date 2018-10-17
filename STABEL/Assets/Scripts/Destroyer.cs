using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= -10)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "destroyer")
        {
            Destruction();
        }
    }

    void Destruction()
    {
        Destroy(this.gameObject);
    }
}
