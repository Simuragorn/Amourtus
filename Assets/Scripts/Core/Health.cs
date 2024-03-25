using Assets.Scripts.Core;
using System;
using UnityEngine;

public abstract class Health : CharacterModule
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    public EventHandler OnDeath;

    protected virtual void Awake()
    {
        health = maxHealth;
    }
    public void GetHit(float damage)
    {
        if (isPaused)
        {
            return;
        }

        health -= damage;
        if (health <= 0)
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
