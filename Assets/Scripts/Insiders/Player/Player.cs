using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Insider
{
    protected PlayerConfiguration PlayerConfiguration => Configuration as PlayerConfiguration;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            TryUseTeleport();
        }
    }
}
