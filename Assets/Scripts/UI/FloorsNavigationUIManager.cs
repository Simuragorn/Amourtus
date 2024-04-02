using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorsNavigationUIManager : MonoBehaviour
{
    public event EventHandler<FloorItem> OnFloorSelected;

    private List<FloorItem> floorItems;
    [SerializeField] private GridLayoutGroup floorsGrid;
    [SerializeField] private FloorItem floorItemPrefab;

    public void SetFloorItems(List<Floor> floors)
    {
        floorItems = new List<FloorItem>();
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

    private void FloorItem_OnSelected(object sender, FloorItem selectedFloor)
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
