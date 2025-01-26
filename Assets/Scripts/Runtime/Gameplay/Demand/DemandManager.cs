using Runtime.Data.Demand;
using Runtime.Gameplay.Demand.UI;
using Runtime.Gameplay.Economy;
using Runtime.Gameplay.Inventory;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Gameplay.Demand
{
    public class DemandManager : Singleton<DemandManager>
    {
        [field: SerializeField] private DemandPanel demandPanelPrefab;
        private DemandPanel demandPanel;
        public DemandPanel DemandPanel
        {
            get
            {
                if (!demandPanel)
                {
                    demandPanel = Instantiate(demandPanelPrefab, GameManager.instance.mainCanvas);
                }
                
                return demandPanel;
            }
        }

        public void DeliverDemand(DemandInfo info)
        {
            info.status = DemandStatus.Delivered;
            GameEconomy.instance.IncreaseCoin(info.revenue);
            foreach (var order in info.orderInfos)
            {
                InventoryManager.instance.DecreaseItemCount(order.productInfo.itemType, order.quantity);
            }
            
        }

        public bool CheckDemandCanBeDelivered(DemandInfo info)
        {
            foreach (var orderInfo in info.orderInfos)
            {
                if (!InventoryManager.instance.IsExistInInventory(orderInfo.productInfo.itemType, orderInfo.quantity))
                {
                    WarningMessageText.Warning?.Invoke("Insufficient Product!");
                    return false;
                }
            }

            return true;
        }
    }
}
