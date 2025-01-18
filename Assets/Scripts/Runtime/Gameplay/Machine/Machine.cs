using Runtime.Data.Machines;
using Runtime.Gameplay.Inventory;
using UnityEngine;

namespace Runtime.Gameplay.Machine
{
    public class Machine : MonoBehaviour
    {
        public MachineInfo machineInfo;
        public bool workInProgress;
        
        public void Produce()
        {
            workInProgress = true;
            UseRawMaterials();
            GetFinalProduct(1);
        }

        private void UseRawMaterials()
        {
            foreach (var requiredMaterialInfo in machineInfo.productInfo.requiredMaterialInfos)
            {
                InventoryManager.instance.Inventory.DecreaseItemCount(requiredMaterialInfo.materialInfo.itemType,
                    requiredMaterialInfo.amount);
            }
        }
        
        private void GetFinalProduct(int count)
        {
            InventoryManager.instance.Inventory.IncreaseItemCount(machineInfo.productInfo.itemType, count);
        }
    }
}
