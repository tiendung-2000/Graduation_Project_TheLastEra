using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerEnemy : MonoBehaviour
{
    public float heathPoint;
    private Animator myAni;
    private Player player;
    private Bullet bullet;

    public float myDamage;
    public float shootingRanger;
    float timeCoolDown;
    public float timeCoolDownSetting; 
    public GameObject bulletShot;
    public GameObject bulletParent;

    EnemyAI enemyAI;

    private void Awake()
    {
        myAni = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    //ham duoi chuyen qua may cai dang bullet...de the nay cung khong sao nha
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //--give damage--------
        if (collision.gameObject.layer == 10)
        {
            player = collision.GetComponent<Player>();
            player.TakeDamage(myDamage);
        }

        //----------------------------Take Damage-----------------
        if (collision.gameObject.layer == 12)
        {
            player = collision.GetComponentInParent<Player>();
            heathPoint -= player.playerDamage;
        }

        if (collision.gameObject.layer == 13)
        {
            bullet = collision.GetComponentInParent<Bullet>();
            heathPoint -= bullet.bulletDamage;
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
        if (heathPoint <= 0)
        {
            myAni.SetBool("Dead", true);
            Destroy(gameObject, 1f);
        }

        if (!PlayerSkill.instance.enemieStopMove)
        {
            //enemie move
        }
    }

    void Update()
    {
        
        if (timeCoolDown <= 0f && enemyAI.canShot)
        {
            GameObject bulletCreate=
                Instantiate(bulletShot, bulletParent.transform.position, Quaternion.identity);
            bulletCreate.GetComponent<BulletEnemy>().myDamage = myDamage;
            timeCoolDown = timeCoolDownSetting;
        }
        else timeCoolDown -= Time.deltaTime;
    }
}
