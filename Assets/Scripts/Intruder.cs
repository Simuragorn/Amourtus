using UnityEngine;

public class Intruder : Character
{
    [SerializeField] IntruderMovement movement;
    public override void TeleportTo(Teleport teleport)
    {
        base.TeleportTo(teleport);
        switch (teleport.Type)
        {
            case TeleportType.Left:
                movement.SetMovingDirection(MovingDirectionType.Right);
                break;
            case TeleportType.Right:
                movement.SetMovingDirection(MovingDirectionType.Left);
                break;
            default:
                break;
        }
    }
}
