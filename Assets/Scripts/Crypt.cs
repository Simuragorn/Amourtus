using Assets.Scripts.Dto;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crypt : MonoBehaviour
{
    [SerializeField] private List<SoulResource> souls = new List<SoulResource>();
    [SerializeField] private int fame;
    [SerializeField] private List<Floor> floors = new List<Floor>();

    private void Awake()
    {
        souls = new List<SoulResource>
        {
            new SoulResource { SoulType= SoulTypeEnum.Small },
            new SoulResource { SoulType= SoulTypeEnum.Medium },
            new SoulResource { SoulType= SoulTypeEnum.Big },
            new SoulResource { SoulType= SoulTypeEnum.Large },
        };
        foreach (var floor in floors)
        {
            floor.OnSoulGet += Floor_OnSoulGet;
        }
    }

    private void Floor_OnSoulGet(object sender, SoulTypeEnum e)
    {
        var soulAmount = souls.First(s => s.SoulType == e);
        soulAmount.Amount++;
    }
}
