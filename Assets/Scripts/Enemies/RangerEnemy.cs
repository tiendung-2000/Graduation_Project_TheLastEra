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
    public GameObject deathEffect;

    EnemyAI enemyAI;

    private void Awake()
    {
        myAni = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

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
            Destroy(gameObject);
            DeathEffect();
        }
    }

    void Update()
    {
        if (timeCoolDown <= 0f && enemyAI.canShot)
        {
            GameObject bulletCreate = Instantiate(bulletShot, bulletParent.transform.position, Quaternion.identity);
            bulletCreate.GetComponent<BulletEnemy>().myDamage = myDamage;
            timeCoolDown = timeCoolDownSetting;
        }
        else timeCoolDown -= Time.deltaTime;
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
        Debug.Log(ItemDropPrefabContainer.Instance == null);
        ItemDropPrefabContainer.Instance.DropItem(transform.position);
        UI_BarManager.instance.AddCoin(10);
    }

}
