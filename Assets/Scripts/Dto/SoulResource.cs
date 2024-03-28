using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dto
{
    public enum SoulTypeEnum
    {
        Small,
        Medium,
        Big,
        Large
    }
    [Serializable]
    public class SoulResource
    {
        public SoulTypeEnum SoulType;
        public int Amount;
    }
}
