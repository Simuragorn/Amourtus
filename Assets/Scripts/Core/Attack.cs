using Assets.Scripts.Core;
using Assets.Scripts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Attack : CharacterModule
{
    [SerializeField] private Collider2D attackRangeCollider;
    [SerializeField] private List<AttackType> attackTypes = new List<AttackType>();

    public event EventHandler<AttackType> OnAttackStarted;

    private float reloadingTimeLeft = 0;
    private Character target = null;
    protected AttackType currentAttack = null;

    protected void Update()
    {
        reloadingTimeLeft = Mathf.Max(reloadingTimeLeft - Time.deltaTime, 0);
        TryAttack();
    }

    public void MakeDamage()
    {
        if (target != null)
        {
            target.GetHit(currentAttack.damage);
        }
    }

    public void FinishAttack()
    {
        reloadingTimeLeft = currentAttack.reloading;
        currentAttack = null;
    }
    public override void Pause(bool disablePhysics)
    {
        base.Pause(disablePhysics);
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
            target = collider.GetComponent<Character>();
        }
    }

    protected void TryRemoveTarget(Collider2D collider)
    {
        if (IsEnemy(collider))
        {
            var lostTarget = collider.GetComponent<Character>();
            if (lostTarget == target)
            {
                target = null;
            }
        }
    }

    protected abstract bool IsEnemy(Collider2D collider);

    protected void TryAttack()
    {
        if (isPaused)
        {
            return;
        }
        if (target != null && currentAttack == null && reloadingTimeLeft <= 0)
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
