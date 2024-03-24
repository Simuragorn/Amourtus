using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Constants.AnimationConstants;

public enum MovingDirectionType
{
    Left,
    Right
}

public class IntruderMovement : Movement
{
    [SerializeField] private MovingDirectionType movingDirection = MovingDirectionType.Left;
    [SerializeField] private Animator animator;

    private void Awake()
    {
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

        if (horizontalAxis == 0)
        {
            animationState = AnimationStateEnum.Idle;
        }
        else
        {
            TryMove(horizontalAxis);
        }
        animator.SetInteger(AnimationStateKey, (int)animationState);
    }
}
