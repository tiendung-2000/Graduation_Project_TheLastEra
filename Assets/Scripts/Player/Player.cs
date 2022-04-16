using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("=====List Weapon=====")]
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
    public float currentHealth;
    public float maxHealthP;
    public float currentMana;
    public float maxManaP;
    public float playerDamage;
    public int currentWeaponIndex;
    public int coin;


    public float coutDownTime;
    public float coutDownTimeSetting = 0.25f;

    public bool attack;
    private bool isSetUp = false;

    UI_BarManager barManager;
    UI_BarController healthBarController;
    UI_BarController manaBarController;
    //UI_BarController shieldBarController;


    private void Awake()
    {
        animatorPlayer = GetComponent<Animator>();
        Debug.Log("Player");
        myBody = GetComponent<Rigidbody2D>();
    }
    private IEnumerator Start()
    {
        yield return null;
        if (GameController.Instance != null)
        {
            this.currentHealth = GameController.Instance.playerCurrentHealth;
            this.maxHealthP = GameController.Instance.maxHealthP;
            this.currentMana = GameController.Instance.playerCurrentMana;
            this.maxManaP = GameController.Instance.maxManaP;
            this.coin = GameController.Instance.playerCoin;
        }//barManager = UI_BarManager.instance;
        healthBarController = UI_BarManager.instance.GetBarController(BarName.P_HealthBar);
        manaBarController = UI_BarManager.instance.GetBarController(BarName.P_ManaBar);
        if (healthBarController != null && manaBarController !=null)
        {
            Debug.Log("healthBarController: " + healthBarController);
            Debug.Log("manaBarController: " + manaBarController);
            isSetUp = true;
        }

    }
    private void Update()
    {
        if (currentHealth > 0)
        {
            Attack();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                switchWeapon();
            }
        }
        if (currentHealth <= 0)
        {
            animatorPlayer.SetBool("PlayerDead", true);
            UI_Manager.instance.GetUI_Canvas(UIName.UIGameOver).OnOpen();
        }
        if (isSetUp)
        {
            healthBarController.OnInit(currentHealth, maxHealthP);
            manaBarController.OnInit(currentMana, maxManaP);
            
            UI_BarManager.instance.UpdateCoin();
            isSetUp = false;
        }
    }



    // Update is called once per frame
    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (currentHealth > 0)
        {
            if (movement != Vector2.zero)
            {
                myBody.MovePosition(myBody.position + movement * movementSpeed * Time.deltaTime);

                animatorPlayer.SetBool("Running", true);
            }
            else
            {
                animatorPlayer.SetBool("Running", false);
            }
            if (coutDownTime > 0f)
                coutDownTime -= Time.deltaTime;

            Flip();
        }
    }

    void Flip()
    {
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

    public void TakeDamage(float enemieDamage, bool heal = false)
    {
        currentHealth -= enemieDamage;
        healthBarController.OnChangeValue(-enemieDamage);
        if (!heal)
            animatorPlayer.SetTrigger("Hit");
    }

    public void ManaChange(float manaChange)
    {
        currentMana -= manaChange;
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

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(coutDownTimeSetting);
        attack = false;
    }

    void switchWeapon()
    {
        if (currentWeaponIndex + 1 < listWeapon.Count)
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
    public void OnReInitHPMana()
    {
        healthBarController.OnInit(currentHealth, maxHealthP);
        manaBarController.OnInit(currentMana, maxManaP);
    }
}
