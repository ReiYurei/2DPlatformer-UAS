using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
   
    public UnitStatus status;
    public Slider slider;

    void Update()
    {
        SetHealth();  
        if (status.HealthPoint <= 0)
        {
            status.PlayerDies();
        }
    }
    public void SetHealth()
    {
       slider.maxValue = status.maxHP;
       slider.value = status.HealthPoint;
    }

}
