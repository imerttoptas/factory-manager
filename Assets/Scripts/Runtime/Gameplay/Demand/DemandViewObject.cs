using Runtime.Gameplay.Input;
using UnityEngine;

namespace Runtime.Gameplay.Demand
{
    public class DemandViewObject : MonoBehaviour , IClickableObject
    {
        public bool CanBeClicked { get; set; }
        public GameObject GameObject => gameObject;

        private void Start()
        {
            CanBeClicked = true;
        }

        public void OnClick()
        {
            DemandManager.instance.DemandPanel.Open();
        }
    }
}
