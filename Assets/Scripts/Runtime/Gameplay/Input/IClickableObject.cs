using UnityEngine;

namespace Runtime.Gameplay.Input
{
    public interface IClickableObject
    {
        public void OnClick();
        public bool CanBeClicked { get; set; }
        public GameObject GameObject { get;}
    }
}