using System;
using TMPro;
using UnityEngine;

public class BattleFloorItem : MonoBehaviour
{
    public event EventHandler<BattleFloorItem> OnSelected;

    [SerializeField] protected TextMeshProUGUI floorItemText;
    [SerializeField] protected GameObject selectedItemBorder;
    protected Floor floor;
    protected bool isSelected;

    public Floor Floor => floor;

    public void SetFloor(Floor currentFloor)
    {
        floor = currentFloor;
        floorItemText.text = floor.Name;
        selectedItemBorder.SetActive(isSelected);
        floor.OnFloorUpdated += Floor_OnFloorUpdated;
    }

    private void Floor_OnFloorUpdated(object sender, EventArgs e)
    {
        if (isSelected)
        {
            OnSelected?.Invoke(this, this);
        }
    }

    public void Select()
    {
        isSelected = !isSelected;
        selectedItemBorder.SetActive(isSelected);
        if (!isSelected)
        {
            OnSelected?.Invoke(this, null);
        }
        else
        {
            OnSelected?.Invoke(this, this);
        }
    }
    public void Deselect()
    {
        isSelected = false;
        selectedItemBorder.SetActive(isSelected);
    }
}
