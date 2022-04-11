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

    public virtual void Start()
    {
        Debug.Log("UI_BarController");  
        OnInit(1, 1);
        UI_BarManager.instance.AddBarController(this);
    }

    public virtual void OnInit( float _currentValue, float _maxValue)
    {
        current_Value = _currentValue;
        max_Value = _maxValue;
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
public interface InitInterface
{
    public void OnInit();
}

