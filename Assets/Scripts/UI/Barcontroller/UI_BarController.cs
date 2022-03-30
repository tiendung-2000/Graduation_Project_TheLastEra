using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarName { E_HealthBar, P_HealthBar, P_ManaBar, P_ShieldBar}

public class UI_BarController : MonoBehaviour
{
    public float current_Value;
    public float max_Value;
    public Image fillBar;
    public BarName barName;
    UI_BarManager ui_BarManager;

    public virtual void Start()
    {
        ui_BarManager = UI_BarManager.instance;
        OnInit(1, 1);
        ui_BarManager.AddBarController(this);
    }
    public virtual void OnInit( float _currentValue, float _maxValue)
    {
        current_Value = _currentValue;
        max_Value = _maxValue;
        OnChangeValue(0);
    }
    public virtual void OnOpen()
    {
        gameObject.SetActive(true);
    }
    public virtual void OnClose()
    {
        gameObject.SetActive(false);
    }
    public virtual void OnChangeValue(float valueChange)
    {
        current_Value += valueChange;
        if (current_Value > max_Value)
            current_Value = max_Value;
        if (current_Value <= 0f)
            fillBar.fillAmount = 0;
        fillBar.fillAmount = current_Value / max_Value;
    }
}
