using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleFloorDetailUIManager : MonoBehaviour
{
    [SerializeField] private BattleIntruderItem intruderItemPrefab;
    [SerializeField] private BattleInsiderItem insiderItemPrefab;
    [SerializeField] private GridLayoutGroup intrudersGrid;
    [SerializeField] private GridLayoutGroup insidersGrid;
    private Floor floor;

    private void Awake()
    {
        intrudersGrid.gameObject.SetActive(false);
        insidersGrid.gameObject.SetActive(false);
    }

    public void SetFloor(Floor currentFloor)
    {
        floor = currentFloor;
        SetIntruders(floor.Intruders.ToList());
        SetInsiders(floor.Insiders.ToList());
        intrudersGrid.gameObject.SetActive(true);
        insidersGrid.gameObject.SetActive(true);
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
    private void SetInsiders(List<Insider> insiders)
    {
        foreach (Transform child in insidersGrid.transform)
        {
            Destroy(child.gameObject);
        }
        var groupedInsiders = insiders.OrderBy(i => i.Configuration.CharacterName).GroupBy(i => i.Configuration.CharacterName);
        foreach (var group in groupedInsiders)
        {
            var insiderItem = Instantiate(insiderItemPrefab, insidersGrid.transform);
            if (group.Any())
            {
                insiderItem.SetInsider(group.FirstOrDefault(), group.Count());
            }
        }
    }
}
