using Runtime.Gameplay.Input;
using UnityEngine;

namespace Runtime.Gameplay.Inventory
{
    public class InventoryViewObject : MonoBehaviour , IClickableObject
    {
        private void Start()
        {
            CanBeClicked = true;
        }

        public void OnClick()
        {
            InventoryManager.instance.InventoryPanel.Open();
        }

        public bool CanBeClicked { get; set; }
        public GameObject GameObject => gameObject;
    }
}
