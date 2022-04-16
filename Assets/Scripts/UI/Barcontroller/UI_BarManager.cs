using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_BarManager : MonoBehaviour
{
    public static UI_BarManager instance;
    public List<UI_BarController> list_BarControllers;
    [SerializeField] private TextMeshProUGUI txtCoin;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            
        }
    }

    public void AddBarController(UI_BarController controller)
    {
        list_BarControllers.Add(controller);
    }

    public UI_BarController GetBarController(BarName barName)
    {
        for(int i = 0; i< list_BarControllers.Count; i++)
        {
            if (list_BarControllers[i].barName == barName)
                return list_BarControllers[i];
        }
        return null;
    }
    public void AddCoin(int coin)
    {
        var player = FindObjectOfType<Player>();
        player.coin += coin;
        UpdateCoin();
    }

    public void UpdateCoin()
    {
        var player = FindObjectOfType<Player>();
        txtCoin.text = player.coin.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddCoin(100);
        }
    }
}
