using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHealthBar : MonoBehaviour
{
   
    private UnitStatus status;
    [SerializeField]public Slider slider;

    public void SetHealth()
    {
        slider.highValue = status.maxHP;
        slider.value = status.HealthPoint;
    }

}
