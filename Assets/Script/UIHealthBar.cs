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
    }
    public void SetHealth()
    {
      //  slider.highValue = status.maxHP;
      //  slider.value = status.HealthPoint;
    }

}
