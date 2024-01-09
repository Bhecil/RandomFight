using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _slider.value = currentHealth / maxHealth;
    }
}
