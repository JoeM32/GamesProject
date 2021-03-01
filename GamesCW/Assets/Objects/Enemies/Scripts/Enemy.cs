using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Transform target;


    protected CharacterController cc;

    public event Action death;
   
    private bool protection = false;

    [SerializeField]
    private int maxHealth;
    protected int currentHealth;
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
                Destroy(gameObject);
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    //ALL PROJECTILE SCRIPTS ARE GARBAGE HERE BE DRAGONS

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(takeDamage(other.GetComponent<Projectile>()));
    }

    IEnumerator takeDamage(Projectile projectile)
    {
        if (projectile.team == "player")
        {
            if (protection)
            {
                yield break;
            }
            else
            {
                Debug.Log("Enemy hurt for " + projectile.damage);
                CurrentHealth -= projectile.damage;
                protection = true;
                yield return new WaitForSeconds(0.2f);
                protection = false;
            }
        }
    }

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
        cc = transform.GetComponent<CharacterController>();
        Debug.Log("Enemy spawned with " + CurrentHealth + "health.");
    }


    public void setPlayer(Transform player)
    {
        this.target = player;
    }
}
