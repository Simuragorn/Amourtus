using UnityEngine;

public enum IntruderResolveStateEnum
{
    Advance,
    Retreat
}
public class Intruder : Character
{
    [SerializeField] IntruderMovement movement;
    [SerializeField] protected float morale;
    protected float maxMorale;
    public IntruderConfiguration IntruderConfiguration => Configuration as IntruderConfiguration;
    protected IntruderResolveStateEnum intruderResolveState;
    protected Floor floor;
    protected override void Awake()
    {
        base.Awake();
        maxMorale = IntruderConfiguration.MaxMorale;
        morale = IntruderConfiguration.MaxMorale;
        isTeleportable = true;
    }
    public void SetFloor(Floor currentFloor)
    {
        floor = currentFloor;
        ChangeMorale(0);
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
        ChangeMorale(-damage);
    }

    protected void ChangeMorale(float change)
    {
        morale += change;
        UpdateResolveState();
        Vector2 movementTarget = GetMovementTarget();
        SetMovementTarget(movementTarget);
    }

    protected void UpdateResolveState()
    {
        float moralePercentage = morale / maxMorale * 100;
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
            movement.SetMovingDirection(MovingDirectionType.Right);
        }
        else
        {
            movement.SetMovingDirection(MovingDirectionType.Left);
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
