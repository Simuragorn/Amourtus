using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeeperFloorsNavigationUIManager : MonoBehaviour
{
    public event EventHandler<KeeperFloorItem> OnFloorSelected;

    private List<KeeperFloorItem> floorItems;
    [SerializeField] private GridLayoutGroup floorsGrid;
    [SerializeField] private KeeperFloorItem floorItemPrefab;

    public void SetFloorItems(List<Floor> floors)
    {
        floorItems = new List<KeeperFloorItem>();
        foreach (Transform child in floorsGrid.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var floor in floors)
        {
            var floorItem = Instantiate(floorItemPrefab, floorsGrid.transform);
            floorItem.OnSelected += FloorItem_OnSelected;
            floorItem.SetFloor(floor);

            floorItems.Add(floorItem);
        }
    }

    private void FloorItem_OnSelected(object sender, KeeperFloorItem selectedFloor)
    {
        OnFloorSelected?.Invoke(this, selectedFloor);
        foreach (var floorItem in floorItems)
        {
            if (floorItem != selectedFloor)
            {
                floorItem.Deselect();
            }
        }
    }
}
