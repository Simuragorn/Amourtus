using UnityEngine;
using UnityEngine.UI;

public class KeeperFloorDetailUIManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup insidersGrid;
    private Floor floor;

    private void Awake()
    {
        insidersGrid.gameObject.SetActive(false);
    }

    public void SetFloor(Floor currentFloor)
    {
        floor = currentFloor;
        insidersGrid.gameObject.SetActive(true);
    }
}
