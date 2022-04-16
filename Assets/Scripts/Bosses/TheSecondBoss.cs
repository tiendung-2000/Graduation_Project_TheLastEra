using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSecondBoss : MonoBehaviour
{
    private Animator bossAni;
    private Player player;
    private Bullet bullet;
    public GameObject bossBullet;
    public GameObject bossShotPoint;
    public GameObject deathEffect;
    public List<Transform> listShotPoint;

    public GameObject bulletSkill2;
    public List<float> skillCD;
    public List<float> skillCDSetting;


    EnemyAI bossAI;

    public float bossHealth;
    public float maxHealth;
    public float bossDamage;
    public float bossShotingRange;

    UI_BarManager barManager;
    UI_BarController healthBarController;

    private void Start()
    {
        barManager = UI_BarManager.instance;
        healthBarController = barManager.GetBarController(BarName.E_HealthBar);
        healthBarController.OnInit(bossHealth, maxHealth);
    }

    private void Awake()
    {
        bossAni = GetComponent<Animator>();
        bossAI = GetComponent<EnemyAI>();
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
        if (collision.gameObject.layer == 12)
        {
            player = collision.GetComponentInParent<Player>();
            ChangeHP(-player.playerDamage);
        }

        if (collision.gameObject.layer == 13)
        {
            bullet = collision.GetComponentInParent<Bullet>();
            ChangeHP(-bullet.bulletDamage);
        }

        if (collision.gameObject.layer == 14)
        {
            float damage = PlayerSkill.instance.skills[1].skillDamage;
            ChangeHP(-damage);
        }

        if (collision.gameObject.layer == 15)
        {
            float damage = PlayerSkill.instance.skills[0].skillDamage;
            ChangeHP(-damage);
        }
    }

    private void FixedUpdate()
    {
        if (bossHealth <= 0)
        {
            bossAni.SetBool("BossDead", true);
            Destroy(gameObject, 0.5f);
            DeathEffect();
        }
    }

    void Update()
    {
        if (bossHealth > 0)
        {
            //Boss Skill Time Count
            for (int i = 0; i < skillCD.Count; i++)
            {
                if (skillCD[i] > 0f)
                    skillCD[i] -= Time.deltaTime;
            }

            if (skillCD[0] <= 0f && bossHealth >= 500f)
                Skill1();

            if (skillCD[1] <= 0f && bossHealth <= 500f)
                Skill2();
        }
    }

    void Skill1()
    {
        //ban thuong        
        GameObject bulletCreate = Instantiate(bossBullet, bossShotPoint.transform.position, Quaternion.identity);
        bulletCreate.GetComponent<BulletEnemy>().myDamage = bossDamage;
        skillCD[0] = skillCDSetting[0];
    }

    void Skill2()
    {
        //ban 4 huong
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
        bossHealth += value;
        if (bossHealth > maxHealth)
            bossHealth = maxHealth;
        if (bossHealth < 0)
            bossHealth = 0;

        healthBarController.OnChangeValue(-value);
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
