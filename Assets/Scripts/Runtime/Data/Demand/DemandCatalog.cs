using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data.Demand
{
    [CreateAssetMenu(fileName = "DemandCatalog",menuName = "Gameplay/DemandCatalog")]
    public class DemandCatalog : ScriptableObject
    {
        public List<DemandInfo> demandInfos;
    }

    [Serializable]
    public class DemandInfo
    {
        public List<OrderInfo> orderInfos;
        public int period;
        public int revenue;
        public string customerName;
        public DemandStatus status;
    }

    public enum DemandStatus
    {
        Passive = 0,
        Active = 1,
        Delivered = 2
    }
}