using System;
using Runtime.Gameplay.Supplier.UI;
using UnityEngine;

namespace Runtime.Gameplay.Supplier
{
    public class SupplierManager : Singleton<SupplierManager>
    {
        [field: SerializeField] private SupplierPanel supplierPanelPrefab;
        private SupplierPanel supplierPanel;
        public SupplierPanel SupplierPanel
        {
            get
            {
                if (!supplierPanel)
                {
                    supplierPanel = Instantiate(supplierPanelPrefab, GameManager.instance.mainCanvas);
                }
                
                return supplierPanel;
            }
        }
    }
}