using Assets.Scripts.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dto
{
    public class CryptSaveData : SaveData
    {
        public List<SoulResource> Souls;
        public int Fame;

        public CryptSaveData()
        {
            Key = SaveConstants.CryptSaveKey;
            Souls = new List<SoulResource>
            {
            new SoulResource { SoulType = SoulTypeEnum.Small },
            new SoulResource { SoulType = SoulTypeEnum.Medium },
            new SoulResource { SoulType = SoulTypeEnum.Big },
            new SoulResource { SoulType = SoulTypeEnum.Large },
            };
        }
    }
}
