using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    quick,
    medium,
    slow,
    /*dodge,
    parry(successful),
    hold,*/
};
public class Player : MonoBehaviour
{
    public event Action<float> healthChange;
    public event Action<float> staminaChange;
    public event Action death;

    public int dodgeRange = 5;

    [SerializeField]
    private int maxHealth;
    private int currentHealth;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (value > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (value < 0)
            {
                death?.Invoke();
            }
            else
            {
                currentHealth = value;
            }
            healthChange?.Invoke((float)currentHealth/(float)maxHealth);
        }
    }

    [SerializeField]
    private int maxStamina;
    private int currentStamina;
    public int CurrentStamina
    {
        get
        {
            return currentStamina;
        }
        set
        {
            if (value > maxStamina)
            {
                currentStamina = maxStamina;
            }
            else if (value < 0)
            {
                currentStamina = 0;
            }
            else
            {
                currentStamina = value;
            }
            if (staminaChange != null)
            {
                staminaChange((float)currentStamina / (float)maxStamina);
            }
        }
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        CurrentStamina = maxStamina;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            CurrentHealth -= 1;
        }
    }

    public void Parry()
    {

    }

}
