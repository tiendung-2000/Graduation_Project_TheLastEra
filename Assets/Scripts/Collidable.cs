using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Interacable();
            Debug.Log("Hieu PC");
        }
    }

    public virtual void Interacable() { }
}
