using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecondPhrase : MonoBehaviour
{
    //GameObject target;
    Rigidbody2D bulletRB;
    private Player player;
    public float speed;
    public float myDamage;

    private void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.velocity = transform.right * speed;
        Destroy(this.gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            player = collision.GetComponent<Player>();
            player.TakeDamage(myDamage);
        }
    }
}
