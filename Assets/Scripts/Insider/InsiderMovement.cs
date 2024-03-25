using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsiderMovement : Movement
{
    [SerializeField] private MovingDirectionType movingDirection = MovingDirectionType.Right;

    protected override void Awake()
    {
        base.Awake();
        movementBlockingLayers = new List<string> { LayersConstants.Obstacle, LayersConstants.Intruder };
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

    private void HandleMovement()
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
