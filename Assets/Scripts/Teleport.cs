using UnityEngine;

public enum TeleportTypeEnum
{
    Start,
    End
}
public class Teleport : MonoBehaviour
{
    public Teleport ConnectedTeleport { get; set; }
    [SerializeField] private Collider2D trigger;
    [SerializeField] private Transform spawnPoint;
    protected TeleportTypeEnum teleportType;
    protected Floor floor;

    public TeleportTypeEnum TeleportType => teleportType;
    public Floor Floor => floor;
    public Transform SpawnPoint => spawnPoint;


    public void Init(Floor currentFloor, TeleportTypeEnum newTeleportType)
    {
        floor = currentFloor;
        teleportType = newTeleportType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null)
        {
            character.ReachedTeleport(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null)
        {
            character.LeftTeleport(this);
        }
    }
}
