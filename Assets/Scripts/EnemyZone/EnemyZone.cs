using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public List<EnemyAI> myEnemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            foreach (EnemyAI enemy in myEnemies)
            {
                if (enemy != null)
                    enemy.playerOnZone = true;
            }
        }
        if (collision.gameObject.layer == 11)
        {
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            enemy.onZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            foreach (EnemyAI enemy in myEnemies)
            {
                enemy.playerOnZone = false;
            }
        }

        if (collision.gameObject.layer == 11)
        {
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            enemy.onZone = false;
        }
    }

}
