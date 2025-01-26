using System.Collections.Generic;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Gameplay.Supplier.UI
{
    public class SupplierPanel : Panel
    {
        [SerializeField] private Transform contentParent;
        [SerializeField] private List<SupplierItemUIObject> uiObjects; 
        public override void Open()
        {
            base.Open();
            cancelButton.onClick.AddListener(Close);
            foreach (var uiObject in uiObjects)
            {
                uiObject.Initialize();
            }
        }
        
        public override void Close()
        {
            RemoveAllListeners();
            base.Close();
        }
    }
}