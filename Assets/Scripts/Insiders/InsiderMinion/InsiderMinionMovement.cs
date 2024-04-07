using Assets.Scripts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InsiderMinionMovement : InsiderMovement
{
    private InsiderMinion insiderMinion => character as InsiderMinion;

    [SerializeField] private MovingDirectionType movingDirection = MovingDirectionType.Right;
    private float peacefulMovementTimeLeft;

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
        PeacefulMovement();
        BattleMovement();
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

    private void PeacefulMovement()
    {
        if (peacefulMovementTimeLeft <= 0 && !insiderMinion.Floor.Intruders.Any())
        {
            int movingDirectionIndex = UnityEngine.Random.Range(-5, 3);
            movingDirection = movingDirectionIndex < 0 ? 
                MovingDirectionType.None : (MovingDirectionType)movingDirectionIndex;
            peacefulMovementTimeLeft = UnityEngine.Random.Range(1, 4);
        }
        peacefulMovementTimeLeft -= Time.deltaTime;
    }
    private void BattleMovement()
    {
        if (insiderMinion.Floor.Intruders.Any())
        {
            List<Vector2> intrudersPositions = insiderMinion.Floor.Intruders.Select(i => (Vector2)i.transform.position).ToList();
            Vector2 closestIntruder = intrudersPositions.OrderBy(intruderPos => Vector2.Distance(transform.position, intruderPos)).FirstOrDefault();
            float xValue = (closestIntruder - (Vector2)transform.position).normalized.x;
            movingDirection = xValue > 0 ?
                MovingDirectionType.Right : MovingDirectionType.Left;
        }
    }
}
