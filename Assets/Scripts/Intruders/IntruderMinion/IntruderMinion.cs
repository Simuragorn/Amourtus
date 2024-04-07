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

}