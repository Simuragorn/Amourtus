using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InsiderMovement : Movement
{

    protected override void Awake()
    {
        base.Awake();
        movementBlockingLayers = new List<string> { LayersConstants.Obstacle, LayersConstants.Intruder };
    }
}
