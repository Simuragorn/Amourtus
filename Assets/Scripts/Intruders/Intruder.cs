using Assets.Scripts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
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

    protected DayPhaseManager dayPhaseManager;
    protected override void Awake()
    {
        base.Awake();
        morale = IntruderConfiguration.MaxMorale;
        dayPhaseManager = FindObjectOfType<DayPhaseManager>();
        if (dayPhaseManager != null)
        {
            dayPhaseManager.OnDayPhaseChanged += Intruder_OnDayPhaseChanged;
            if (dayPhaseManager.CurrentPhase == DayPhaseEnum.Night ||
                dayPhaseManager.CurrentPhase == DayPhaseEnum.LateNight)
            {
                SetMoraleToZero();
            }
        }
    }

    protected void OnDestroy()
    {
        if (floor != null)
        {
            floor.OnFloorUpdated -= CurrentFloor_OnFloorUpdated;
        }
        if (dayPhaseManager != null)
        {
            dayPhaseManager.OnDayPhaseChanged -= Intruder_OnDayPhaseChanged;
        }
        PauseModules(true);
        floor.RemoveIntruder(this, IntruderRemovingConditionEnum.Retreat);
    }

    public override void SetFloor(Floor currentFloor)
    {
        base.SetFloor(currentFloor);
        currentFloor.OnFloorUpdated += CurrentFloor_OnFloorUpdated;
        currentFloor.AddIntruder(this);

    }

    public override void ReachedTeleport(Teleport teleport)
    {
        base.ReachedTeleport(teleport);
        Teleport from = availableTeleport;
        if (
            (from.TeleportType == TeleportTypeEnum.Start &&
            intruderResolveState == IntruderResolveStateEnum.Advance) ||

            (from.TeleportType == TeleportTypeEnum.End &&
            intruderResolveState == IntruderResolveStateEnum.Retreat) ||
            floor.Insiders.Any())
        {
            return;
        }
        TryUseTeleport();
    }
    public override void TryUseTeleport()
    {
        Debug.Log("Use teleport");
        base.TryUseTeleport();
        if (availableTeleport != null && availableTeleport.ConnectedTeleport != null)
        {
            var destinationFloor = availableTeleport.ConnectedTeleport.Floor;
            availableTeleport = null;
            floor.OnFloorUpdated -= CurrentFloor_OnFloorUpdated;
            floor.RemoveIntruder(this, IntruderRemovingConditionEnum.FloorChange);
            SetFloor(destinationFloor);
        }
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

    protected void Intruder_OnDayPhaseChanged(object sender, DayPhaseEnum phase)
    {
        if (dayPhaseManager.CurrentPhase == DayPhaseEnum.Night ||
                dayPhaseManager.CurrentPhase == DayPhaseEnum.LateNight)
        {
            SetMoraleToZero();
        }
    }

    protected void SetMoraleToZero()
    {
        ChangeMorale(-morale);
    }

    protected override void HealthModule_OnDeath(object sender, EventArgs eventArgs)
    {
        base.HealthModule_OnDeath(sender, eventArgs);
        floor.RemoveIntruder(this, IntruderRemovingConditionEnum.Death);
    }

    private void CurrentFloor_OnFloorUpdated(object sender, EventArgs e)
    {
        UpdateTarget();
        if (availableTeleport != null)
        {
            ReachedTeleport(availableTeleport);
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
        Vector2? enemyTarget = GetMovementEnemyTarget();
        if (enemyTarget != null)
        {
            return enemyTarget.Value;
        }
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

    private Vector2? GetMovementEnemyTarget()
    {
        if (Floor.Insiders.Any())
        {
            List<Vector2> insidersPositions = Floor.Insiders.Select(i => (Vector2)i.transform.position).ToList();
            Vector2 closestInsider = insidersPositions.OrderBy(insiderPos => Vector2.Distance(transform.position, insiderPos)).FirstOrDefault();
            return closestInsider;
        }
        return null;
    }
}
