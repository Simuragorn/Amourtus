using Assets.Scripts.Core;
using System;
using UnityEngine;

public abstract class Health : CharacterModule
{
    protected float maxHealth;
    protected float health;
    public EventHandler OnDeath;

    public override void Init(Character currentCharacter)
    {
        base.Init(currentCharacter);
        maxHealth = character.Configuration.MaxHealth;
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
