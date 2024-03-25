using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Constants.AnimationConstants;

public abstract class Movement : CharacterModule
{
    public event EventHandler<Vector2> OnMovementStarted;
    protected float minMovementDistance = 1f;
    protected float maxMovementDistance = 2f;
    protected float movementDistance;
    [SerializeField] protected float movementSpeed = 4f;
    [SerializeField] protected Collider2D collider;
    [SerializeField] protected Rigidbody2D rigidbody;
    protected List<string> movementBlockingLayers = new List<string>();

    private const float movementDelay = 0.3f;
    private float movementDelayLeft;
    private bool previouslyMovementAllowed = true;

    public override void Pause(bool disablePhysics)
    {
        base.Pause(disablePhysics);
        if (disablePhysics)
        {
            rigidbody.isKinematic = true;
            collider.enabled = false;
        }
    }

    public override void Resume()
    {
        base.Resume();
        rigidbody.isKinematic = false;
        collider.enabled = true;
    }

    protected virtual void Awake()
    {
        movementDistance = UnityEngine.Random.Range(minMovementDistance, maxMovementDistance);
    }

    protected virtual void TryMove(float horizontalAxis)
    {
        if (isPaused)
        {
            return;
        }

        if (character.AnimationState != AnimationStateEnum.Idle &&
            character.AnimationState != AnimationStateEnum.Move)
        {
            return;
        }

        if (horizontalAxis != 0)
        {
            character.Flip(horizontalAxis < 0);
        }
        var direction = new Vector2(horizontalAxis, 0);
        bool movementAllowed = CanMove(direction);
        if (movementAllowed && !previouslyMovementAllowed)
        {
            movementDelayLeft = movementDelay;
        }
        Vector2 translation = default;
        if (movementAllowed && movementDelayLeft <= 0)
        {
            translation = movementSpeed * Time.deltaTime * direction;
        }
        OnMovementStarted?.Invoke(this, translation);
        previouslyMovementAllowed = movementAllowed;
    }

    protected virtual void Update()
    {
        movementDelayLeft = Mathf.Max(movementDelayLeft - Time.deltaTime, 0);
    }

    protected virtual bool CanMove(Vector2 direction)
    {
        int layerMask = LayerMask.GetMask(movementBlockingLayers.ToArray());
        RaycastHit2D hit = Physics2D.Raycast(collider.bounds.center, direction, movementDistance, layerMask);
        return hit.collider == null;
    }
}
