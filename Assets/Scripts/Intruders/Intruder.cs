using Assets.Scripts.Dto;
using System;
using UnityEngine;
using static Assets.Scripts.Constants.AnimationConstants;

public enum IntruderResolveStateEnum
{
    Advance,
    Retreat
}
public abstract class Intruder : Character
{
    private IntruderMovement intruderMovementModule => movementModule as IntruderMovement;
    private IntruderHealth intruderHealthModule => healthModule as IntruderHealth;
    private IntruderAttack intruderAttackModule => attackModule as IntruderAttack;

    protected float morale;
    public IntruderConfiguration IntruderConfiguration => Configuration as IntruderConfiguration;

    protected IntruderResolveStateEnum intruderResolveState;
    protected override void Awake()
    {
        base.Awake();
        morale = IntruderConfiguration.MaxMorale;
        isTeleportable = true;
    }

    protected override void HealthModule_OnDeath(object sender, EventArgs eventArgs)
    {
        base.HealthModule_OnDeath(sender, eventArgs);
        floor.RemoveIntruder(this);
    }

    protected void OnDestroy()
    {
        floor.RemoveIntruder(this);
    }

    public override void SetFloor(Floor currentFloor)
    {
        base.SetFloor(currentFloor);
        currentFloor.AddIntruder(this);
        UpdateTarget();
    }

    public override void TakeHit(float damage)
    {
        if (!healthModule.IsPaused)
        {
            tookHit = true;
            healthModule.GetHit(damage);
            ChangeMorale(-damage);
            if (!isDead)
            {
                PauseModules(false);
                ChangeAnimationState(AnimationStateEnum.TakeHit);
            }
        }     
    }

    protected void ChangeMorale(float change)
    {
        morale += change;
        UpdateResolveState();
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        Vector2 movementTarget = GetMovementTarget();
        SetMovementTarget(movementTarget);
    }

    protected void UpdateResolveState()
    {
        float moralePercentage = morale / IntruderConfiguration.MaxMorale * 100;
        if (moralePercentage > 50)
        {
            intruderResolveState = IntruderResolveStateEnum.Advance;
        }
        else
        {
            intruderResolveState = IntruderResolveStateEnum.Retreat;
        }
    }

    protected Vector2 GetMovementTarget()
    {
        if (intruderResolveState == IntruderResolveStateEnum.Retreat)
        {
            return floor.FloorStartTeleport.transform.position;
        }
        else
        {
            return floor.FloorEndTeleport.transform.position;
        }
    }
    protected void SetMovementTarget(Vector2 target)
    {
        if (target.x > transform.position.x)
        {
            intruderMovementModule.SetMovingDirection(MovingDirectionType.Right);
        }
        else
        {
            intruderMovementModule.SetMovingDirection(MovingDirectionType.Left);
        }
    }
    public override void Teleport(Teleport from, Teleport to)
    {
        if (
            (from.TeleportType == TeleportTypeEnum.Start &&
            intruderResolveState == IntruderResolveStateEnum.Advance) ||

            (from.TeleportType == TeleportTypeEnum.End &&
            intruderResolveState == IntruderResolveStateEnum.Retreat))
        {
            return;
        }
        base.Teleport(from, to);
        floor.RemoveIntruder(this);
        if (to != null)
        {
            SetFloor(to.Floor);
        }
    }
}
