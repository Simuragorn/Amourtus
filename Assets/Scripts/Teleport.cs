using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport ConnectedTeleport { get; set; }
    [SerializeField] private Collider2D trigger;
    [SerializeField] private Transform spawnPoint;
    public Transform SpawnPoint => spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null && character.TeleportationAvailable)
        {
            character.TeleportTo(ConnectedTeleport.SpawnPoint.position);
        }
    }
}
