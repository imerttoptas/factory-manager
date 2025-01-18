using UnityEngine;

namespace Runtime.Data.Machines
{
    [CreateAssetMenu(fileName = "MachineInfo",menuName = "Gameplay/MachineInfo")]
    public class MachineInfo : ScriptableObject
    {
        public Sprite icon;
        public ProductInfo productInfo;
        public int requiredTime;
    }
}
