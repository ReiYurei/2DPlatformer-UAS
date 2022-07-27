using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{

    public PlayerMoveController player;
    public UnitStatus status;
    public Slider slider;
    public Slider sliderHP;
    public Slider sliderJump;

    public Gradient gradient;
    public Image fill;

    void LateUpdate()
    {
        SetHealth();
        if (status.HealthPoint <= 0)
        {
            status.PlayerDie();
        }
        SetJump();
    }

    public void SetJump()
    {
        slider.maxValue = status.maxHP;
        slider.value = status.HealthPoint;
        sliderJump.maxValue = 2;
        sliderJump.value = player.CurrentAttackCount;
        fill.color = gradient.Evaluate(sliderJump.normalizedValue);
    }

    public void SetHealth()
    {
        sliderHP.maxValue = status.maxHP;
        sliderHP.value = status.HealthPoint;
    }
}