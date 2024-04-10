using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dto
{
    public enum CoinTypeEnum
    {
        Copper,
        Silver,
        Gold,
        Platinum
    }
    [Serializable]
    public class CoinResource
    {
        public CoinTypeEnum CoinType;
        public int Amount;
    }
}
