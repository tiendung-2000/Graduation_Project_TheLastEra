using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [Header("UI Element")]
    [SerializeField] private TextMeshProUGUI txtHealthMax;
    [SerializeField] private TextMeshProUGUI txtManaMax;
    [SerializeField] private TextMeshProUGUI txtDamageMax;

    public float maxHealthPerUpgrade = 10;
    public float maxManaPerUpgrade = 10;
    public float maxDamagePerUpgrade = 10;

    //test
    public int coinPerUpgrade = 100;

    private static UI_Upgrade _instance;
    public static UI_Upgrade Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            OnOpen();
        }
    }
    public void OnClose()
    {
        panel.SetActive(false);
    }
    public void OnOpen()
    {
        panel.SetActive(true);
        UpdateMaxHealth();
        UpdateMaxMana();
    }

    public void UpdateMaxHealth()
    {
        var player = FindObjectOfType<Player>();
        txtHealthMax.text = player.maxHealthP.ToString();
    }
    public void OnUpgradeMaxHealth()
    {
        var player = FindObjectOfType<Player>();
        if (player.coin < coinPerUpgrade)
            return;
        player.maxHealthP += maxHealthPerUpgrade;
        UpdateMaxHealth();
        player.OnReInitHPMana();
        UI_BarManager.instance.AddCoin(coinPerUpgrade * -1);
    }

    public void UpdateMaxMana()
    {
        var player = FindObjectOfType<Player>();
        txtManaMax.text = player.maxManaP.ToString();
    }
    public void OnUpgradeMaxMana()
    {
        var player = FindObjectOfType<Player>();
        if (player.coin < coinPerUpgrade)
            return;
        player.maxManaP += maxManaPerUpgrade;
        UpdateMaxMana();
        player.OnReInitHPMana();
        UI_BarManager.instance.AddCoin(coinPerUpgrade * -1);
    }
}
