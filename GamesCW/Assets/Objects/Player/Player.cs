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
    public Projectile projectile;


    CharacterController cc;
    public bool infiniteStamina = false;
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
    private float staminaRegenSpeed = 0.1f;
    [SerializeField]
    private int staminaRegenAmount = 1;
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

            if (infiniteStamina == true)
            {
                currentStamina = maxStamina;
            }

            if (staminaChange != null)
            {
                staminaChange((float)currentStamina / (float)maxStamina);//change to invoke
            }
        }
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        CurrentStamina = maxStamina;
        cc = transform.GetComponent<CharacterController>();
        StartCoroutine(StaminaRegeneration());


        //Bodge fix do not ship lmao gonna do it anyway but fr dont forget about this!!!!!! dunno why Unity aint updating inspector value

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


    //MASSIVE REWORK REQUIRED BELOW!!!!!!!!!!


    public void Attack(Vector3 direction, float time)//all nees to be encloded in Weapon which is its own scriptable object, with its own coroutines and prefab hitboxes.
    {
        Debug.Log("Attacking towards - " + direction);//direction is NOT realtive to player yet, it is simple the ponit in space the attack should face
        if(time < 0.1f && CurrentStamina >= 20)
        {
            Debug.Log("Swipe Attack");
            CurrentStamina -= 20;
            StartCoroutine(Swipe(direction - transform.position));
        }
        else if(time < 1f && CurrentStamina >= 30)//would like to subtract the window from Swipe from time, allwoing you to set this as a window of 2
        {
            Debug.Log("Shoot Attack");
            CurrentStamina -= 30;
            StartCoroutine(Shoot(direction - transform.position));
        }
        else if (currentStamina >= 60)
        {
            Debug.Log("Beam Attack");
            CurrentStamina -= 60;
            StartCoroutine(Beam(direction - transform.position));
        }


    }

    IEnumerator Swipe(Vector3 direction)
    {
        float timer = 0;
        Projectile projectileInstance = Instantiate(projectile, transform.position + direction.normalized, Quaternion.LookRotation(direction.normalized, Vector3.up));
        projectileInstance.speed = 2;
        projectileInstance.damage = 60;
        projectileInstance.transform.localScale *= 2;
        projectileInstance.team = "player";
        while (timer < 0.2f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(projectileInstance.gameObject);
        
    }

    IEnumerator Shoot(Vector3 direction)
    {
        float timer = 0;
        Projectile projectileInstance = Instantiate(projectile, transform.position + direction.normalized, Quaternion.LookRotation(direction.normalized, Vector3.up));
        projectileInstance.speed = 8;
        projectileInstance.damage = 70;
        projectileInstance.team = "player";
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(projectileInstance.gameObject);
    }

    IEnumerator Beam(Vector3 direction)
    {
        float timer = 0;
        Projectile projectileInstance = Instantiate(projectile, transform.position + direction.normalized, Quaternion.LookRotation(direction.normalized, Vector3.up));
        projectileInstance.transform.localScale *= 4;
        projectileInstance.speed = 4;
        projectileInstance.damage = 200;
        projectileInstance.team = "player";
        while (timer < 5f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(projectileInstance.gameObject);
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
    //ALL PROJECTILE SCRIPTS ARE GARBAGE HERE BE DRAGONS

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(takeDamage(other.GetComponent<Projectile>()));
    }

    IEnumerator takeDamage(Projectile projectile)
    {
        if (projectile.team == "enemy" && dodging == false && parrying == false)
        {
            Debug.Log("Player hurt for " + projectile.damage);
            CurrentHealth -= projectile.damage;

        }
        yield break;
    }
}
