using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleFloorDetailUIManager : MonoBehaviour
{
    [SerializeField] private BattleIntruderItem intruderItemPrefab;
    [SerializeField] private GridLayoutGroup intrudersGrid;
    private Floor floor;

    private void Awake()
    {
        intrudersGrid.gameObject.SetActive(false);
    }

    public void SetFloor(Floor currentFloor)
    {
        floor = currentFloor;
        SetIntruders(floor.Intruders.ToList());
        intrudersGrid.gameObject.SetActive(true);
    }

    private void SetIntruders(List<Intruder> intruders)
    {
        foreach (Transform child in intrudersGrid.transform)
        {
            Destroy(child.gameObject);
        }
        var groupedIntruders = intruders.OrderBy(i => i.Configuration.CharacterName).GroupBy(i => i.Configuration.CharacterName);
        foreach (var group in groupedIntruders)
        {
            var intruderItem = Instantiate(intruderItemPrefab, intrudersGrid.transform);
            if (group.Any())
            {
                intruderItem.SetIntruder(group.FirstOrDefault(), group.Count());
            }
        }
    }
}
