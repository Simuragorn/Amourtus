using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float teleportationReloading = 1f;
    [SerializeField] private float height = 4f;
    private float teleportationReloadingLeft;
    private bool teleportationAvailable = true;
    public bool TeleportationAvailable => teleportationAvailable;

    public void TeleportTo(Vector2 spawnPoint)
    {
        teleportationAvailable = false;
        teleportationReloadingLeft = teleportationReloading;
        gameObject.transform.position = new Vector2(spawnPoint.x, spawnPoint.y + height);
    }

    private void Update()
    {
        teleportationReloadingLeft = Mathf.Max(0, teleportationReloadingLeft - Time.deltaTime);
        teleportationAvailable = teleportationReloadingLeft <= 0;
    }
}
