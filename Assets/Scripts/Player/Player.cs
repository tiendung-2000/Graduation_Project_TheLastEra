using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour 
{
    [Header ("=====List Weapon=====")]
    [Space(15)]
    public List<GameObject> listWeapon;


    private Rigidbody2D myBody;
    private Vector2 movement;
    private Animator animatorPlayer;
    private BulletEnemy enemyBulletDamage;
    private BossSkill2 firstBossSkill;

    [Header("=====Player Information=====")]
    [Space(15)]
    public float movementSpeed = 4f;
    public float playerHealth;
    public float maxHealthP;
    public float playerMana;
    public float maxManaP;
    public float playerDamage;
    public int currentWeaponIndex;


    public float coutDownTime;
    public float coutDownTimeSetting = 0.25f;

    public bool attack;

    UI_BarManager barManager;
    UI_BarController healthBarController;
    UI_BarController manaBarController;
    //UI_BarController shieldBarController;


    private void Awake()
    {
        animatorPlayer = GetComponent<Animator>();
    }

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        barManager = UI_BarManager.instance;

        healthBarController = barManager.GetBarController(BarName.P_HealthBar);
        manaBarController = barManager.GetBarController(BarName.P_ManaBar);

        healthBarController.OnInit(playerHealth, maxHealthP);
        manaBarController.OnInit(playerMana, maxManaP);
    }

    private void Update()
    {
        if(playerHealth > 0)
        {
            Attack();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                switchWeapon();
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(playerHealth > 0)
        {
            if (movement != Vector2.zero)
            {
                myBody.MovePosition(myBody.position + movement * movementSpeed * Time.deltaTime);
            
                animatorPlayer.SetBool("Running", true);
            }
            else {
                animatorPlayer.SetBool("Running", false);
            }
            if (coutDownTime > 0f)
                coutDownTime -= Time.deltaTime;

            Flip();
        }

        if(playerHealth <= 0)
        {
            animatorPlayer.SetBool("PlayerDead", true);
            UI_Manager.instance.GetUI_Canvas(UIName.UIGameOver).OnOpen();
        }
    }

    void Flip() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if ((mousePos.x > transform.position.x && transform.localScale.x < 0) || 
            (mousePos.x < transform.position.x && transform.localScale.x > 0))
        {
            Vector3 scaleTemp = transform.localScale;
            scaleTemp.x *= -1;
            transform.localScale = scaleTemp;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && coutDownTime <= 0f)
        {
            animatorPlayer.SetTrigger("Attack");
            coutDownTime = coutDownTimeSetting;
            attack = true;
            StartCoroutine(ResetAttack());
        }
    }

    public void TakeDamage(float enemieDamage)
    {
        playerHealth -= enemieDamage;
        healthBarController.OnChangeValue(-enemieDamage);
        animatorPlayer.SetTrigger("Hit");
    }

    public void ManaChange(float manaChange)
    {
        playerMana -= manaChange;
        manaBarController.OnChangeValue(-manaChange);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 17)
        {
            enemyBulletDamage = collision.GetComponent<BulletEnemy>();
            TakeDamage(enemyBulletDamage.myDamage);
            Destroy(collision.gameObject);
            animatorPlayer.SetTrigger("Hit");
        }

        if (collision.gameObject.layer == 18)
        {
            Destroy(collision.gameObject);
            animatorPlayer.SetTrigger("Hit");
        }

    }

    IEnumerator ResetAttack() {
        yield return new WaitForSeconds(coutDownTimeSetting);
        attack = false;
    }

    void switchWeapon()
    {
        if(currentWeaponIndex + 1 < listWeapon.Count)
        {
            listWeapon[currentWeaponIndex].SetActive(false);
            currentWeaponIndex++;
            listWeapon[currentWeaponIndex].SetActive(true);
        }
        else
        {
            listWeapon[currentWeaponIndex].SetActive(false);
            currentWeaponIndex = 0;
            listWeapon[currentWeaponIndex].SetActive(true);
        }
    }
}
