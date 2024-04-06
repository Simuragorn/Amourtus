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
    [SerializeField] private int fame;
    [SerializeField] private List<Floor> floors = new List<Floor>();

    public IReadOnlyList<Floor> Floors => floors;
    public IReadOnlyList<SoulResource> Souls => souls;
    public event EventHandler OnSoulsCountChanged;

    private void Awake()
    {
        LoadData();
        foreach (var floor in floors)
        {
            floor.OnSoulGet += Floor_OnSoulGet;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var saveData = new CryptSaveData { Souls = souls, Fame = fame };
            SaveData(saveData);
        }
    }

    private void Floor_OnSoulGet(object sender, SoulTypeEnum e)
    {
        var soulAmount = souls.First(s => s.SoulType == e);
        soulAmount.Amount++;
        OnSoulsCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TryAddInsider(InsiderConfiguration insiderConfiguration, Floor floor)
    {
        SoulResource soulResource = Souls.First(s => s.SoulType == insiderConfiguration.SoulType);
        if (soulResource.Amount > insiderConfiguration.Cost)
        {
            Insider insiderPrefab = insidersPrefabs.Find(i => i.Configuration.CharacterName == insiderConfiguration.CharacterName);
            Insider newInsider = Instantiate(insiderPrefab, floor.FloorEndTeleport.SpawnPoint.position, Quaternion.identity, floor.transform);

            floor.AddInsider(newInsider);
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
        fame = saveData.Fame;
    }

    public void SaveData(CryptSaveData data)
    {
        SaveManager.Save(SaveConstants.CryptSaveKey, data);
    }
}
