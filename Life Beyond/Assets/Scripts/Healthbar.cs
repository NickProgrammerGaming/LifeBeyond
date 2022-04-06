using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text hpValueText;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        hpValueText.text = slider.value + "/" + slider.maxValue;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        hpValueText.text = slider.value + "/" + slider.maxValue;
    }
    
}
