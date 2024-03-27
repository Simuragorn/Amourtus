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
