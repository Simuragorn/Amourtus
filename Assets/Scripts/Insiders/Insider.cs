using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Insider : Character
{
    private InsiderMovement insiderMovementModule => movementModule as InsiderMovement;
    private InsiderHealth insiderHealthModule => healthModule as InsiderHealth;
    private InsiderMinionAttack insiderAttackModule => attackModule as InsiderMinionAttack;

    public InsiderConfiguration InsiderConfiguration => Configuration as InsiderConfiguration;

    protected override void HealthModule_OnDeath(object sender, EventArgs eventArgs)
    {
        base.HealthModule_OnDeath(sender, eventArgs);
        if (floor != null)
        {
            floor.RemoveInsider(this);
        }
    }
}
