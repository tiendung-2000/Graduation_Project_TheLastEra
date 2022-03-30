using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BudgetManager : MonoBehaviour
{
    public static BudgetManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    private int coin;
    public TextMeshProUGUI coinText;
    public Button coint;

    private void Start()
    {
        coint.onClick.AddListener(()=>ChangeCoin(10000));
    }

    public int m_coin { 
        get { return coin; } 

        set { 
            ChangeCoin(value);
        }
    }

    void ChangeCoin(int value) {
        coin += value;
        if (coin < 0)
            coin = 0;

        coinText.text = coin.ToString();
    }
}
