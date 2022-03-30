using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
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
        if(collision.gameObject.layer == 12)
        {
            player = collision.GetComponentInParent<Player>();
            heathPoint -= player.playerDamage;
        }

        if (collision.gameObject.layer == 13)
        {
            bullet = collision.GetComponentInParent<Bullet>();
            heathPoint -= bullet.bulletDamage;
        }

        if (collision.gameObject.layer == 10)
        {
            player = collision.GetComponent<Player>();
            player.TakeDamage(myDamage);
        }

        if (collision.gameObject.layer == 14)
        {
            float damage = PlayerSkill.instance.skills[1].skillDamage;
            heathPoint -= damage;
        }

        if (collision.gameObject.layer == 15)
        {
            float damage = PlayerSkill.instance.skills[0].skillDamage;
            heathPoint -= damage;
        }
    }

    private void FixedUpdate()
    {
        if(heathPoint <= 0)
        {
            myAni.SetBool("Dead",true);
            Destroy(gameObject,1f);
        }

        if (!PlayerSkill.instance.enemieStopMove)
        {
            //enemie move
        }
    }
}
