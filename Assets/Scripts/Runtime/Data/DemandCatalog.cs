using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data
{
    [CreateAssetMenu(fileName = "DemandCatalog",menuName = "Gameplay/DemandCatalog")]
    public class DemandCatalog : ScriptableObject
    {
        public List<PeriodInfo> periodInfos;
    }

    [Serializable]
    public class PeriodInfo
    {
        public List<OrderInfo> orderInfos;
        public int period;
    }
}