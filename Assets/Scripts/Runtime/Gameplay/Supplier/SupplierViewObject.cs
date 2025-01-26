using Runtime.Gameplay.Input;
using UnityEngine;

namespace Runtime.Gameplay.Supplier
{
    public class SupplierViewObject :  MonoBehaviour , IClickableObject
    {
        public bool CanBeClicked { get; set; }
        public GameObject GameObject => gameObject;

        private void Start()
        {
            CanBeClicked = true;
        }

        public void OnClick()
        {
            SupplierManager.instance.SupplierPanel.Open();
        }
    }
}