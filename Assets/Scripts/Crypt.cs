using Assets.Scripts.Constants;
using Assets.Scripts.Core;
using Assets.Scripts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crypt : MonoBehaviour, IContainSaveData<CryptSaveData>
{
    [SerializeField] private List<InsiderMinion> insidersPrefabs;
    [SerializeField] private List<SoulResource> souls = new List<SoulResource>();
    [SerializeField] private List<CoinResource> coins = new List<CoinResource>();
    [SerializeField] private int fame;
    [SerializeField] private List<Floor> floors = new List<Floor>();

    public IReadOnlyList<Floor> Floors => floors;
    public IReadOnlyList<SoulResource> Souls => souls;
    public IReadOnlyList<CoinResource> Coins => coins;
    public event EventHandler OnSoulsCountChanged;
    public event EventHandler OnCoinsCountChanged;

    private void Awake()
    {
        LoadData();
        InitFloors();
    }

    private void InitFloors()
    {
        Floor entranceFloor = floors.First();
        Floor firstUndegroundFloor = floors[floors.IndexOf(entranceFloor) + 1];
        Floor throneFloor = floors.Last();

        for (int i = 0; i < floors.Count; i++)
        {
            Floor floor = floors[i];
            FloorTypeEnum floorType = FloorTypeEnum.Common;
            if (i == 0)
            {
                floorType = FloorTypeEnum.Entrance;
            }
            if (i == floors.Count - 1)
            {
                floorType = FloorTypeEnum.Throne;
            }
            floor.OnSoulGet += Floor_OnSoulGet;
            floor.OnCoinGet += Floor_OnCoinGet;
            
            Floor previousFloor = i > 0 ? floors[i - 1] : null;
            Floor nextFloor = i < floors.Count - 1 ? floors[i + 1] : null;
            
            floor.Init(this, previousFloor, nextFloor, floorType);
            floor.gameObject.SetActive(floor.IsPurchased);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var saveData = new CryptSaveData { Souls = souls, Coins = coins, Fame = fame };
            SaveData(saveData);
        }
    }

    private void Floor_OnSoulGet(object sender, SoulTypeEnum e)
    {
        var soulsAmount = souls.First(s => s.SoulType == e);
        soulsAmount.Amount++;
        OnSoulsCountChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Floor_OnCoinGet(object sender, CoinTypeEnum e)
    {
        var coinsAmount = coins.First(c => c.CoinType == e);
        coinsAmount.Amount++;
        OnCoinsCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TryAddInsider(InsiderConfiguration insiderConfiguration, Floor floor)
    {
        SoulResource soulResource = Souls.First(s => s.SoulType == insiderConfiguration.SoulType);
        if (soulResource.Amount >= insiderConfiguration.Cost)
        {
            Insider insiderPrefab = insidersPrefabs.Find(i => i.Configuration.CharacterName == insiderConfiguration.CharacterName);
            Insider newInsider = Instantiate(insiderPrefab, floor.FloorEndTeleport.SpawnPoint.position, Quaternion.identity, floor.transform);

            floor.AddInsider(newInsider);
            newInsider.SetFloor(floor);
            soulResource.Amount -= insiderConfiguration.Cost;
            OnSoulsCountChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void TryRemoveInsider(InsiderConfiguration insiderConfiguration, Floor floor)
    {
        Insider existingInsider = floor.Insiders.FirstOrDefault(i => i.Configuration.CharacterName == insiderConfiguration.CharacterName);
        if (existingInsider != null)
        {
            floor.RemoveInsider(existingInsider);
            existingInsider.gameObject.SetActive(false);
            Destroy(existingInsider);
            SoulResource soulResource = Souls.First(s => s.SoulType == insiderConfiguration.SoulType);
            soulResource.Amount += insiderConfiguration.Cost;
            OnSoulsCountChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void LoadData()
    {
        var saveData = SaveManager.Load<CryptSaveData>(SaveConstants.CryptSaveKey);
        souls = saveData.Souls;
        coins = saveData.Coins;
        fame = saveData.Fame;
    }

    public void SaveData(CryptSaveData data)
    {
        SaveManager.Save(SaveConstants.CryptSaveKey, data);
    }
}
