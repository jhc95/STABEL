using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private float hp = 1;
    // Update is called once per frame
    void Update()
    {
        /*        if (this.transform.position.y <= -10)
                {
                    Destroy(this.gameObject);
                }
        */
    
        if (hp <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            hp--;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
