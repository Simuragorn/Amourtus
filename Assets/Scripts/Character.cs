using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float teleportationReloading = 1f;
    [SerializeField] private float spawnVerticalOffset = 0f;
    private float teleportationReloadingLeft;
    private bool teleportationAvailable = true;
    public bool TeleportationAvailable => teleportationAvailable;

    public virtual void TeleportTo(Teleport teleport)
    {
        Vector2 spawnPoint = teleport.SpawnPoint.position;
        teleportationAvailable = false;
        teleportationReloadingLeft = teleportationReloading;
        gameObject.transform.position = new Vector2(spawnPoint.x, spawnPoint.y + spawnVerticalOffset);
    }

    protected virtual void Update()
    {
        teleportationReloadingLeft = Mathf.Max(0, teleportationReloadingLeft - Time.deltaTime);
        teleportationAvailable = teleportationReloadingLeft <= 0;
    }
}
