using System.Collections.Generic;
using UnityEngine;

public class FloorsUIManager : MonoBehaviour
{
    [SerializeField] private FloorsNavigationUIManager floorsNavigationUIManager;
    [SerializeField] private FloorDetailUIManager floorDetailUIManager;

    public void Init(List<Floor> floors)
    {
        floorDetailUIManager.gameObject.SetActive(false);

        floorsNavigationUIManager.SetFloorItems(floors);
        floorsNavigationUIManager.OnFloorSelected += FloorsNavigationUIManager_OnFloorSelected;
    }

    private void FloorsNavigationUIManager_OnFloorSelected(object sender, FloorItem floorItem)
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
