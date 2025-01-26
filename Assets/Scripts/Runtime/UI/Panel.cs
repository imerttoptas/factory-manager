using Runtime.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] protected Button actionButton;
        [SerializeField] protected Button cancelButton;

        public virtual void Open()
        {
            gameObject.SetActive(true);
            GameManager.instance.ChangeGameState(GameState.InputDisabled);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        protected void RemoveAllListeners()
        {
            GameManager.instance.ChangeGameState(GameState.Playing);
            actionButton?.onClick.RemoveAllListeners();
            cancelButton?.onClick.RemoveAllListeners();
        }
    }
}
