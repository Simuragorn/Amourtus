using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class IntruderMinion : Intruder
{
    protected IntruderMinionMovement intruderMinionMovement => movementModule as IntruderMinionMovement;
    private IntruderMinionHealth intruderMinionHealthModule => healthModule as IntruderMinionHealth;
    private IntruderMinionAttack intruderMinionAttackModule => attackModule as IntruderMinionAttack;

    public IntruderMinionConfiguration IntruderMinionConfiguration => Configuration as IntruderMinionConfiguration;

    public override void ReachedTeleport(Teleport teleport)
    {
        base.ReachedTeleport(teleport);
        Teleport from = availableTeleport;
        if (
            (from.TeleportType == TeleportTypeEnum.Start &&
            intruderResolveState == IntruderResolveStateEnum.Advance) ||

            (from.TeleportType == TeleportTypeEnum.End &&
            intruderResolveState == IntruderResolveStateEnum.Retreat))
        {
            return;
        }
        TryUseTeleport();
    }
    public override void TryUseTeleport()
    {
        Teleport to = availableTeleport.ConnectedTeleport;
        base.TryUseTeleport();
        floor.RemoveIntruder(this, IntruderRemovingConditionEnum.FloorChange);
        if (to != null)
        {
            SetFloor(to.Floor);
        }
    }
}