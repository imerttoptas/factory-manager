using System.Collections.Generic;
using Runtime.Data.Demand;
using Runtime.Gameplay.Economy;
using Runtime.Gameplay.Inventory;
using Runtime.Pooling;
using Runtime.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.Demand.UI
{
    public class DemandInfoUIObject : MonoBehaviour, IPoolable
    {
        [SerializeField] private Transform productIconParent;
        [SerializeField] private TextMeshProUGUI customerNameText;
        [SerializeField] private TextMeshProUGUI revenueText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button deliverButton;
        private List<OrderUIViewObject> orderUIViewObjects = new();
        private DemandInfo info;
        
        public void Initialize(DemandInfo demandInfo)
        {
            info = demandInfo;
            revenueText.text = info.revenue.ToString();
            customerNameText.text = info.customerName;
            SetButtons();
            InitializeOrderUIViewObjects(info.orderInfos);
        }

        private void InitializeOrderUIViewObjects(List<OrderInfo> orderInfos)
        {
            foreach (var orderInfo in orderInfos)
            {
                OrderUIViewObject orderUIViewObject =
                    PoolFactory.instance.GetObject<OrderUIViewObject>(ObjectType.OrderViewUIObject,
                        parent: productIconParent);
                orderUIViewObject.Initialize(orderInfo);
                orderUIViewObjects.Add(orderUIViewObject);
            }
        }

        private void SetButtons()
        {
            acceptButton.gameObject.SetActive(info.status == DemandStatus.Passive);
            acceptButton.onClick.AddListener(AcceptButtonAction);
            deliverButton.gameObject.SetActive(info.status == DemandStatus.Active);
            deliverButton.onClick.AddListener(DeliverButtonAction);
        }

        private void AcceptButtonAction()
        {
            info.status = DemandStatus.Active;
            acceptButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            deliverButton.gameObject.SetActive(true);
        }
        
        private void DeliverButtonAction()
        {
            if (DemandManager.instance.CheckDemandCanBeDelivered(info))
            {
                DemandManager.instance.DeliverDemand(info);
                info.status = DemandStatus.Delivered;
                gameObject.SetActive(false);
                OnReset();
            }
        }
        private void OnDisable()
        {
            acceptButton.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();
            deliverButton.onClick.RemoveAllListeners();
            foreach (var orderUIViewObject in orderUIViewObjects)
            {
                orderUIViewObject.ResetObject();
            }
        }

        [field: SerializeField] public ObjectType ObjectType { get; set; }
        public void OnSpawn() { }
        public void OnReset() { }
    }
}
