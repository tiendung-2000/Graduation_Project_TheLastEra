using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUsed : MonoBehaviour
{   
    public Item item;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }
}
