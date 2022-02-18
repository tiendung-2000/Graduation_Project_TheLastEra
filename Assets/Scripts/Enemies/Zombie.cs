using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float heathPoint;
    private Animator myAni;
    private Player player;
    private Bullet bullet;
    public float myDamage;

    private void Awake()
    {
        myAni = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.layer == 12)
        //{
        //    player = collision.GetComponentInParent<Player>();
        //    heathPoint -= player.playerDamage;
        //}

        if (collision.gameObject.layer == 12)
        {
            bullet = collision.GetComponentInParent<Bullet>();
            heathPoint -= bullet.bulletDamage;
        }

        if (collision.gameObject.layer == 10)
        {
            player = collision.GetComponent<Player>();
            player.TakeDamage(myDamage);
        }
    }

    private void FixedUpdate()
    {
        if(heathPoint <= 0)
        {
            myAni.SetBool("Dead",true);
            //Destroy(gameObject,1f);
        }
    }
}
