using System.Collections.Generic;
using Runtime.Data.Demand;
using Runtime.Pooling;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Gameplay.Demand.UI
{
    public class DemandPanel : Panel
    {
        [SerializeField] private Transform contentParent;
        [SerializeField] private List<DemandInfoUIObject> uiObjects;
        [SerializeField] private DemandCatalog demandCatalog;
        
        public override void Open()
        {
            base.Open();
            cancelButton.onClick.AddListener(Close);
            foreach (var demandInfo in demandCatalog.demandInfos)
            {
                DemandInfoUIObject uiObject =
                    PoolFactory.instance.GetObject<DemandInfoUIObject>(ObjectType.DemandInfoUIObject,
                        parent: contentParent);
                uiObject.Initialize(demandInfo);
                uiObjects.Add(uiObject);
            }
        }

        public override void Close()
        {
            RemoveAllListeners();
            base.Close();
            foreach (var uiObject in uiObjects)
            {
                uiObject.ResetObject();
            }
        }
    }
}
