using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float playerCurrentHealth;
    public float maxHealthP;
    public float playerCurrentMana;
    public float maxManaP;
    public int playerCoin;

    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
