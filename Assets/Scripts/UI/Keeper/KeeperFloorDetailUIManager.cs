using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeeperFloorDetailUIManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup insidersGrid;
    [SerializeField] private KeeperInsiderItem keeperInsiderItemPrefab;
    [SerializeField] private List<InsiderConfiguration> insidersConfigurationsPrefabs;
    private Floor floor;
    private Crypt crypt;

    private void Awake()
    {
        insidersGrid.gameObject.SetActive(false);
        crypt = FindAnyObjectByType<Crypt>();
    }

    private void SetFloorInsiders()
    {
        foreach (Transform child in insidersGrid.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var configurationPrefab in insidersConfigurationsPrefabs)
        {
            KeeperInsiderItem insiderItem = Instantiate(keeperInsiderItemPrefab, insidersGrid.transform);
            int count = floor.Insiders.Where(i => i.Configuration.CharacterName == configurationPrefab.CharacterName).Count();
            insiderItem.SetInsider(configurationPrefab, count);

            insiderItem.OnTryAddInsider += InsiderItem_OnTryAddInsider;
            insiderItem.OnTryRemoveInsider += InsiderItem_OnTryRemoveInsider;
        }
    }

    private void InsiderItem_OnTryRemoveInsider(object sender, InsiderConfiguration insiderConfigurationType)
    {
        crypt.TryRemoveInsider(insiderConfigurationType, floor);
        SetFloorInsiders();
    }

    private void InsiderItem_OnTryAddInsider(object sender, InsiderConfiguration insiderConfigurationType)
    {
        crypt.TryAddInsider(insiderConfigurationType, floor);
        SetFloorInsiders();
    }

    public void SetFloor(Floor currentFloor)
    {
        floor = currentFloor;
        SetFloorInsiders();
        insidersGrid.gameObject.SetActive(true);
    }
}
