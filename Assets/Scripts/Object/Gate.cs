using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
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
            ScenesManager.instance.ChangeScene(nextScene);
        }
    }
}
