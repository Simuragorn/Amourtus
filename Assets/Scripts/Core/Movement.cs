using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Constants.AnimationConstants;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float minMovementDistance = 1f;
    [SerializeField] protected float movementSpeed = 4f;
    [SerializeField] protected Collider2D collider;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected AnimationStateEnum animationState = AnimationStateEnum.Idle;
    protected List<string> movementBlockingLayers = new List<string>();

    private const float movementDelay = 0.3f;
    private float movementDelayLeft;
    private bool previouslyMovementAllowed = true;

    protected virtual void TryMove(float horizontalAxis)
    {
        spriteRenderer.flipX = horizontalAxis < 0;
        var direction = new Vector2(horizontalAxis, 0);
        bool movementAllowed = CanMove(direction);
        if (movementAllowed && !previouslyMovementAllowed)
        {
            movementDelayLeft = movementDelay;
        }
        if (movementAllowed && movementDelayLeft <= 0)
        {
            transform.Translate(movementSpeed * Time.deltaTime * direction);
            animationState = AnimationStateEnum.Move;
        }
        else
        {
            animationState = AnimationStateEnum.Idle;
        }
        previouslyMovementAllowed = movementAllowed;
    }

    protected virtual void Update()
    {
        movementDelayLeft = Mathf.Max(movementDelayLeft - Time.deltaTime, 0);
    }

    protected virtual bool CanMove(Vector2 direction)
    {
        int layerMask = LayerMask.GetMask(movementBlockingLayers.ToArray());
        RaycastHit2D hit = Physics2D.Raycast(collider.bounds.center, direction, minMovementDistance, layerMask);
        return hit.collider == null;
    }
}
