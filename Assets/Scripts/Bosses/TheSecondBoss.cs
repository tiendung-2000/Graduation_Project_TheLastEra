using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSecondBoss : MonoBehaviour
{
    private Animator bossAni;
    private Player player;
    private Bullet bullet;
    public GameObject bossBullet;
    public GameObject bossShootPoint;
    public GameObject deathEffect;
    public EnemyHealthBarBehavior enemyHealthBar;

    public List<Transform> listShotPoint;

    public GameObject bulletSkill2;
    public List<float> skillCD;
    public List<float> skillCDSetting;


    EnemyAI bossAI;

    public float currentHealth;
    public float maxHealth;
    public float bossDamage;
    public float bossShotingRange;

    private void Awake()
    {
        bossAni = GetComponent<Animator>();
        bossAI = GetComponent<EnemyAI>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        enemyHealthBar.SetHealthBar(currentHealth, maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //--give damage--------
        if (collision.gameObject.layer == 10)
        {
            player = collision.GetComponent<Player>();
            player.TakeDamage(bossDamage);
            bossAni.SetBool("BossAttack", true);
        }

        //----------------------------Take Damage-----------------
        //PlayerWeapon
        if (collision.gameObject.layer == 12)
        {
            player = collision.GetComponentInParent<Player>();
            ChangeHP(-player.playerDamage);
        }
        //PlayerBullet
        if (collision.gameObject.layer == 13)
        {
            bullet = collision.GetComponentInParent<Bullet>();
            ChangeHP(-bullet.bulletDamage);
        }
        //PlayerUltimate
        if (collision.gameObject.layer == 14)
        {
            float damage = PlayerSkill.instance.skills[1].skillDamage;
            ChangeHP(-damage);
        }
        //PlayerSkill
        if (collision.gameObject.layer == 15)
        {
            float damage = PlayerSkill.instance.skills[0].skillDamage;
            ChangeHP(-damage);
        }
    }

    private void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            bossAni.SetBool("BossDead", true);
            Destroy(gameObject, 0.5f);
            DeathEffect();
        }
    }

    void Update()
    {
        if (currentHealth > 0 && bossAI.canShot)
        {
            //Boss Skill Time Count
            for (int i = 0; i < skillCD.Count; i++)
            {
                if (skillCD[i] > 0f)
                    skillCD[i] -= Time.deltaTime;
            }

            if (skillCD[0] <= 0f && currentHealth >= 500f)
                NormalShooting();

            if (skillCD[1] <= 0f && currentHealth <= 500f)
                FourDirectionsShooting();
        }
    }

    void NormalShooting()
    {
        //Normal Shooting       
        GameObject bulletCreate = Instantiate(bossBullet, bossShootPoint.transform.position, Quaternion.identity);
        bulletCreate.GetComponent<BulletEnemy>().myDamage = bossDamage;
        skillCD[0] = skillCDSetting[0];
    }

    void FourDirectionsShooting()
    {
        //Shoot 4 direction 
        float angle = 90;
        for (int i = 0; i < listShotPoint.Count; i++)
        {
            angle -= 90;
            GameObject bulletCreated = Instantiate(bulletSkill2,
                listShotPoint[i].position,
                Quaternion.identity, listShotPoint[i]);
            bulletCreated.transform.eulerAngles = new Vector3(0, 0, angle);
        }
        skillCD[1] = skillCDSetting[1];
        bossAni.SetTrigger("BossAttack");
    }

    public void ChangeHP(float value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentHealth < 0)
            currentHealth = 0;
        enemyHealthBar.SetHealthBar(currentHealth, maxHealth);
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}
