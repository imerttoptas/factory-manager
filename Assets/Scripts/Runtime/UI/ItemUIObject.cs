using Runtime.Data;
using Runtime.Data.Products;
using Runtime.Pooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class ItemUIObject : MonoBehaviour , IPoolable
    {
        [field: SerializeField] public ObjectType ObjectType { get; set; }
        [SerializeField] private Button actionButton;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI countText;
        
        public void Initialize(ProductInfo productInfo, int count)
        {
            icon.sprite = productInfo.icon;
            countText.text = count.ToString();
            actionButton.onClick.AddListener(() => OnClick(productInfo));
        }

        private void OnClick(ProductInfo productInfo)
        {
            foreach (var requiredMaterialInfo in productInfo.requiredMaterialInfos)
            {
                Debug.Log("Required Material Info -> " + requiredMaterialInfo.materialInfo.itemType);
            }
        }

        public void OnSpawn()
        {
            throw new System.NotImplementedException();
        }

        public void OnReset()
        {
            throw new System.NotImplementedException();
        }
    }
}
