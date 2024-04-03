using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFloorsNavigationUIManager : MonoBehaviour
{
    public event EventHandler<BattleFloorItem> OnFloorSelected;

    private List<BattleFloorItem> floorItems;
    [SerializeField] private GridLayoutGroup floorsGrid;
    [SerializeField] private BattleFloorItem floorItemPrefab;

    public void SetFloorItems(List<Floor> floors)
    {
        floorItems = new List<BattleFloorItem>();
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

    private void FloorItem_OnSelected(object sender, BattleFloorItem selectedFloor)
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
