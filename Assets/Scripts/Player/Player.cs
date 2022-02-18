using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour 
{ 
    public float movementSpeed = 4f;

    private Rigidbody2D myBody;
    private Vector2 movement;
    private Animator animatorPlayer;
    public float playerDamage;
    public float playerHealth;

    public float coutDownTime;
    public float coutDownTimeSetting = 0.25f;

    public bool attack;

    private void Awake()
    {
        animatorPlayer = GetComponent<Animator>();
    }

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Attack();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero && attack == false)
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

        if(playerHealth <= 0)
        {
            animatorPlayer.SetBool("PlayerDead", true);
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
    }

    IEnumerator ResetAttack() {
        yield return new WaitForSeconds(coutDownTimeSetting);
        attack = false;
    }
}
