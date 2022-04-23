using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelManager : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public float radiusCheck;
    public Transform checkPoint;
    public int nextScene;

    bool CheckPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(checkPoint.position, radiusCheck, whatIsPlayer);
        if(collider != null)
        {
            Debug.Log("Gate Check Player");
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPoint.position, radiusCheck);
    }

    private void FixedUpdate()
    {
        if (CheckPlayer())
        {
            SavePlayerData();
            ScenesManager.instance.ChangeScene(nextScene);
        }
    }
    private void SavePlayerData()
    {
        var player = FindObjectOfType<Player>();
        var gc = GameController.Instance;
        gc.playerCurrentHealth = player.currentHealth;
        gc.maxHealthP = player.maxHealthP;
        gc.playerCurrentMana = player.currentMana;
        gc.maxManaP = player.maxManaP;
        gc.playerCoin = player.coin;
        Debug.Log("Save Player Data");
    }
}
