using Assets.Scripts.Constants;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MovingDirectionType
{
    None,
    Left,
    Right
}

public abstract class IntruderMovement : Movement
{
    protected MovingDirectionType movingDirection;
    protected Intruder intruder => character as Intruder;

    protected override void Awake()
    {
        base.Awake();
        movementBlockingLayers = new List<string> { LayersConstants.Obstacle, LayersConstants.Insider };
    }
    public void SetMovingDirection(MovingDirectionType newMovingDirection)
    {
        movingDirection = newMovingDirection;
    }
    protected override void Update()
    {
        base.Update();
        HandleMovement();
    }

    protected void HandleMovement()
    {
        float horizontalAxis = 0;
        switch (movingDirection)
        {
            case MovingDirectionType.Left:
                horizontalAxis = -1;
                break;
            case MovingDirectionType.Right:
                horizontalAxis = 1;
                break;
            default:
                break;
        }
        TryMove(horizontalAxis);
    }
}
