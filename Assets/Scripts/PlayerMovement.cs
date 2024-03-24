using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Constants.AnimationConstants;

public class PlayerMovement : Movement
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        movementBlockingLayers = new List<string> { LayersConstants.Obstacle, LayersConstants.Intruder };
    }

    protected override void Update()
    {
        base.Update();
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
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
