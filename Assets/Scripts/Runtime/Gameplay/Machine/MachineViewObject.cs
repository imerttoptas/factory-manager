using Runtime.Gameplay.Input;
using Runtime.Gameplay.Machine.UI;
using UnityEngine;

namespace Runtime.Gameplay.Machine
{
    public class MachineViewObject :  MonoBehaviour , IClickableObject
    {
        public bool CanBeClicked { get; set; }
        public GameObject GameObject => gameObject;
        
        [field: SerializeField] private MachinePanel machinePanelPrefab;
        private MachinePanel machinePanel;
        private MachinePanel MachinePanel
        {
            get
            {
                if (!machinePanel)
                {
                    machinePanel = Instantiate(machinePanelPrefab, GameManager.instance.mainCanvas);
                }
                
                return machinePanel;
            }
        }

        private void Start()
        {
            CanBeClicked = true;
        }

        public void OnClick()
        {
            MachinePanel.Open();
        }
    }
}