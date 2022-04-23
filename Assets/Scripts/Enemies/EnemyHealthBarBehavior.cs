using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarBehavior : MonoBehaviour
{
    public Slider slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    public void SetHealthBar(float currentHealth, float maxHealth)
    {
        slider.gameObject.SetActive(true);
        slider.value = currentHealth;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponent<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }
}
