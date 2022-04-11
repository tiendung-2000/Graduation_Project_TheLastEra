using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public List<Skill> skills;
    public List<float> currentCoolDown;
    public List<float> manaCost;
    public Player player;
    public Transform SkillPoint;
    public GameObject bulletUltimate;
    public GameObject shurikenSkill;
    public LayerMask whatIsEnemies;
    public float shurikenForce = 10f;
    public bool enemieStopMove;
    public float radius;

    public static PlayerSkill instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentCoolDown[0] <= 0)
        {
            if(player.currentMana >= 5)
            {
                player.ManaChange(manaCost[0]);
                ShurikenSkill();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentCoolDown[1] <= 0)
        {
            if (player.currentMana >= 25)
            {
                player.ManaChange(manaCost[0]);
                ShurikenSkill();
            }
            Ultimate();
        }

        if(currentCoolDown[1] > 0)
        {
            currentCoolDown[1] -= Time.deltaTime;
        }
    }

    void ShurikenSkill()
    {
        GameObject shuriken = Instantiate(shurikenSkill, SkillPoint.position, shurikenSkill.transform.rotation);
        Rigidbody2D rb = shuriken.GetComponent<Rigidbody2D>();

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        shuriken.transform.eulerAngles = new Vector3(0, 0, rotZ - 90);

        rb.AddForce(shuriken.transform.up * shurikenForce, ForceMode2D.Impulse);
        Destroy(shuriken, 1f);
    }

    void Ultimate()
    {
        Collider2D[] hitEnemie = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemies);
        if(hitEnemie.Length != 0)
        {
            currentCoolDown[1] = skills[1].cooldownSkill;
            enemieStopMove = true;
            foreach (Collider2D hit in hitEnemie)
            {
                GameObject ultimateCreated = Instantiate(bulletUltimate, hit.transform.position, Quaternion.identity);
                Destroy(ultimateCreated, 1f);
            }
            StartCoroutine(ResetStopMove());         
        }
    }

    IEnumerator ResetStopMove()
    {
        yield return new WaitForSeconds(1f);
        enemieStopMove = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
