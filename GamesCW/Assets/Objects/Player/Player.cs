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
    CharacterController cc;
    public bool immortality = false;

    [SerializeField]
    private AnimationCurve dodgeCurve;
    private bool dodging = false;

    public float ParryWindow = 0.5f;
    private bool parrying = false;

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
            else if (value < 0 && immortality != true)
            {
                death?.Invoke();
            }
            else
            {
                currentHealth = value;
            }
            healthChange?.Invoke((float)currentHealth / (float)maxHealth);
        }
    }

    [SerializeField]
    private float staminaRegenSpeed = 1;
    [SerializeField]
    private int staminaRegenAmount = 10;
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
        cc = transform.GetComponent<CharacterController>();
        StartCoroutine(StaminaRegeneration());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            CurrentHealth -= 1;
        }
    }

    public void Parry()
    {
        StartCoroutine("Parrying");//convert all of this garbage
    }

    public void Dodge(Vector3 position)
    {
        if (CurrentStamina >= 20)//wouldl ike SpendStamina() to return true or false and correclty remove the right amount.
        {
            CurrentStamina -= 20;
            StartCoroutine(Dodging(position));
        }
        else
        {
            Debug.Log("Not enough stamina");
        }
    }

    public void Block(float time)
    {

    }

    public void Attack(Vector3 direction, float time)
    {
        Debug.Log("Attacking towards - " + direction);//direction is NOT realtive to player yet, it is simple the ponit in space the attack should face
    }

    IEnumerator Parrying()
    {
        parrying = true;
        yield return new WaitForSeconds(ParryWindow);
        parrying = false;
    }

    IEnumerator StaminaRegeneration()
    {
        while (true)
        {
            CurrentStamina += staminaRegenAmount;
            yield return new WaitForSeconds(staminaRegenSpeed);
        }
        
    }

    IEnumerator Dodging(Vector3 target)
    {
        dodging = true;
        float timer = 0;
        Vector3 origin = transform.position;
        do
        {
            Vector3 position = Vector3.Lerp(origin, target, dodgeCurve.Evaluate(timer));
            cc.Move(position - transform.position);
            transform.position = position;

            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        while (dodgeCurve.Evaluate(timer) < 1);
        dodging = false;
    }
}
