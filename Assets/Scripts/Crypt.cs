using Assets.Scripts.Constants;
using Assets.Scripts.Core;
using Assets.Scripts.Dto;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crypt : MonoBehaviour, IContainSaveData<CryptSaveData>
{
    [SerializeField] private List<SoulResource> souls = new List<SoulResource>();
    [SerializeField] private int fame;
    [SerializeField] private List<Floor> floors = new List<Floor>();

    public IReadOnlyList<Floor> Floors => floors;

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
