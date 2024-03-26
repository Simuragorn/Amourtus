using Assets.Scripts.Core;
using Assets.Scripts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Attack : CharacterModule
{
    [SerializeField] protected Collider2D attackRangeCollider;
    public event EventHandler<AttackType> OnAttackStarted;

    protected List<AttackType> attackTypes = new List<AttackType>();
    protected List<Character> targets = new List<Character>();
    protected float reloadingTimeLeft = 0;
    protected Character currentTarget = null;
    protected AttackType currentAttack = null;

    public override void Init(Character currentCharacter)
    {
        base.Init(currentCharacter);
        attackTypes = character.Configuration.AttackTypes;
    }

    protected void Update()
    {
        reloadingTimeLeft = Mathf.Max(reloadingTimeLeft - Time.deltaTime, 0);
        TryAttack();
    }

    public void MakeDamage()
    {
        if (currentTarget != null && currentAttack != null)
        {
            currentTarget.GetHit(currentAttack.damage);
        }
    }

    public void FinishAttack()
    {
        if (currentAttack != null)
        {
            reloadingTimeLeft = currentAttack.reloading;
            currentAttack = null;
        }
    }
    public override void Pause(bool disablePhysics)
    {
        base.Pause(disablePhysics);
        currentAttack = null;
        if (disablePhysics)
        {
            attackRangeCollider.enabled = false;
        }
    }

    public override void Resume()
    {
        base.Resume();
        attackRangeCollider.enabled = true;
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        TrySetTarget(collider);
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        TryRemoveTarget(collider);
    }

    protected void TrySetTarget(Collider2D collider)
    {
        if (IsEnemy(collider))
        {
            var newTarget = collider.GetComponent<Character>();
            if (!targets.Contains(newTarget))
            {
                targets.Add(newTarget);
            }
            if (currentTarget == null)
            {
                currentTarget = newTarget;
            }
        }
    }

    protected void TryRemoveTarget(Collider2D collider)
    {
        if (IsEnemy(collider))
        {
            var lostTarget = collider.GetComponent<Character>();
            if (targets.Contains(lostTarget))
            {
                targets.Remove(lostTarget);
            }
            if (lostTarget == currentTarget)
            {
                currentTarget = targets.FirstOrDefault();
            }
        }
    }

    protected abstract bool IsEnemy(Collider2D collider);

    protected void TryAttack()
    {
        if (isPaused || !character.CanAttack)
        {
            return;
        }
        if (currentTarget != null && currentAttack == null && reloadingTimeLeft <= 0)
        {
            if (attackTypes.Any())
            {
                currentAttack = GetRandomAttack();
                OnAttackStarted?.Invoke(this, currentAttack);
            }
        }
    }

    protected AttackType GetRandomAttack()
    {
        int attackIndex = UnityEngine.Random.Range(0, attackTypes.Count);
        return attackTypes[attackIndex];
    }
}
