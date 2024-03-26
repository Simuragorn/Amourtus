using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;

public enum MovingDirectionType
{
    Left,
    Right
}

public class IntruderMovement : Movement
{
    [SerializeField] private MovingDirectionType movingDirection = MovingDirectionType.Left;

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
