using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _mySlider;

    private void Start()
    {
        _mySlider = GetComponent<Slider>();
        PlayerHealth.OnDamage += Redraw;

    }

    private void Redraw(float value)
    {
        _mySlider.value = value;
    }
}
