using System.Collections.Generic;
using UnityEngine;

public class KeeperFloorsUIManager : MonoBehaviour
{
    [SerializeField] private KeeperFloorsNavigationUIManager floorsNavigationUIManager;
    [SerializeField] private KeeperFloorDetailUIManager floorDetailUIManager;

    public void Init(List<Floor> floors)
    {
        floorDetailUIManager.gameObject.SetActive(false);

        floorsNavigationUIManager.SetFloorItems(floors);
        floorsNavigationUIManager.OnFloorSelected += FloorsNavigationUIManager_OnFloorSelected;
    }

    private void FloorsNavigationUIManager_OnFloorSelected(object sender, KeeperFloorItem floorItem)
    {
        if (floorItem == null)
        {
            floorDetailUIManager.gameObject.SetActive(false);
        }
        else
        {
            floorDetailUIManager.SetFloor(floorItem.Floor);
            floorDetailUIManager.gameObject.SetActive(true);
        }
    }
}
