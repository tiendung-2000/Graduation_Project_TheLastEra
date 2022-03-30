using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Store : Collidable
{
    public static NPC_Store Instance;
    public GameObject shopPanel;
    public GameObject bagPanel;
    public bool openShop;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Shop()
    {
        shopPanel.SetActive(true);
        bagPanel.SetActive(true);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F) && openShop){
        //    Shop();
        //}
    }

    public override void Interacable()
    {
        base.Interacable();
        openShop = true;
    }
}