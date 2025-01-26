using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.Economy.UI
{
    public class CalendarIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dateText;
        [SerializeField] private Button forwardButton;
        
        public void Start()
        {
            SetText(GameManager.instance.GameTime);
            GameManager.instance.OnGameTimeChanged += SetText;
            forwardButton.onClick.AddListener(() => GameManager.instance.ForwardGameTime(1));
        }
        
        private void OnDisable()
        {
            GameManager.instance.OnGameTimeChanged -= SetText;
            forwardButton.onClick.RemoveAllListeners();
        }
        
        private void SetText(DateTime dateTime)
        {
            dateText.text = dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year;
        }
    }
}
