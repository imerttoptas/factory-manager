using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Runtime.UI
{
    public class WarningMessageText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageText;
        private Vector3 messageTextCachedPosition;
        public static System.Action<string> Warning;
        public static System.Action<string, Vector3, float> WarningWithPositionAndDuration;

        private Sequence sequence;

        private void OnEnable()
        {
            Warning += ShowMessage;
            WarningWithPositionAndDuration += ShowMessage;
        }

        private void OnDisable()
        {
            Warning -= ShowMessage;
            WarningWithPositionAndDuration -= ShowMessage;
        }

        private void ShowMessage(string message)
        {
            if (messageTextCachedPosition == Vector3.zero)
            {
                messageTextCachedPosition = messageText.transform.localPosition;
            }
        
            float duration = Mathf.Clamp(message.Length / 10, 1.5f, 4f);

            ShowMessage(message, messageTextCachedPosition, duration);
        }
    
        private void ShowMessage(string message, Vector3 position, float duration)
        {
            var moveDistance = 1f;
            var scaleDuration = .3f;
            var holdDuration = duration * 0.67f;
            var fadeOutDuration = duration * 0.33f;
        
            sequence?.Kill();
            messageText.transform.DOKill();
            messageText.DOKill();

            messageText.text = message;
            messageText.transform.localPosition = position;
            messageText.transform.localScale = Vector3.zero;

            sequence = DOTween.Sequence();
            sequence.Append(messageText.transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack));
            sequence.Join(messageText.transform.DOMoveY(moveDistance, duration).SetEase(Ease.InQuad).SetRelative());
            sequence.Join(messageText.DOFade(0f, fadeOutDuration).From(1f).SetEase(Ease.InQuad).SetDelay(holdDuration));
            sequence.OnKill(() =>
            {
                messageText.transform.localScale = Vector3.zero;
                messageText.alpha = 0;
            });
        }
    }
}
