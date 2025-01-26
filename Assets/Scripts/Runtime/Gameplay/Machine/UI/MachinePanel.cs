using System.Collections.Generic;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Gameplay.Machine.UI
{
    public class MachinePanel : Panel
    {
        [SerializeField] private Transform contentParent;
        [SerializeField] private List<MachineProductUIObject> uiObjects;

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
