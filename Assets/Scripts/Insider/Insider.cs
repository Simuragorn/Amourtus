using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insider : Character
{
    protected InsiderConfiguration InsiderConfiguration => Configuration as InsiderConfiguration;
    protected override void Awake()
    {
        base.Awake();
        isTeleportable = false;
    }
}
