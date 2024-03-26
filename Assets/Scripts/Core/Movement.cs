using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : CharacterModule
{
    public event EventHandler<Vector2> OnMovementStarted;
    protected float minMovementDistance = 1f;
    protected float maxMovementDistance = 2f;
    protected float movementDistance;
    protected float movementSpeed;
    [SerializeField] protected Collider2D collider;
    [SerializeField] protected Rigidbody2D rigidbody;
    protected List<string> movementBlockingLayers = new List<string>();

    private const float movementDelay = 0.3f;
    private float movementDelayLeft;
    private bool previouslyMovementAllowed = true;

    public override void Init(Character currentCharacter)
    {
        base.Init(currentCharacter);
        movementSpeed = currentCharacter.Configuration.MovementSpeed;
    }

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
        if (isPaused || !character.CanMove)
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
