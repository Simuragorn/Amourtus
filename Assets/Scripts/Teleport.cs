using UnityEngine;

public enum TeleportType
{
    Left,
    Right
}

public class Teleport : MonoBehaviour
{
    public Teleport ConnectedTeleport { get; set; }
    [SerializeField] private Collider2D trigger;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TeleportType type;
    public Transform SpawnPoint => spawnPoint;
    public TeleportType Type => type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null && character.TeleportationAvailable)
        {
            character.TeleportTo(ConnectedTeleport);
        }
    }
}
