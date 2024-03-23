using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private Teleport floorStartTeleport;
    [SerializeField] private Teleport floorEndTeleport;
    [SerializeField] private Floor nextFloor;
    [SerializeField] private Floor previousFloor;

    private void Awake()
    {
        if (previousFloor != null)
        {
            floorStartTeleport.ConnectedTeleport = previousFloor.floorEndTeleport;
        }
        if (nextFloor != null)
        {
            floorEndTeleport.ConnectedTeleport = nextFloor.floorStartTeleport;
        }
    }
}
