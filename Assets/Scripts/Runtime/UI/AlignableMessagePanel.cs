using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Runtime.Pooling;

namespace UI
{
	public class AlignableMessagePanel : MonoBehaviour, IPoolable
	{
		public enum Position
		{
			AutoVertical,
			AutoHorizontal,
			Above,
			Below,
			Right,
			Left
		}

		[Flags]
		public enum CloseOption
		{
			Input,
			Time
		}

		protected bool DoesCloseOptionMatch(CloseOption currentOption, CloseOption askedOption)
		{
			return (currentOption & askedOption) == askedOption;
		}
		[field: SerializeField] public ObjectType Type { get; set; }
		public bool IsInThePool { get; set; }
		[SerializeField] protected RectTransform panel;
		[SerializeField] protected TextMeshProUGUI messageText;
		[SerializeField] protected LayoutElement messageTextLayoutElement;
	
		#region Pin
		[Foldout("Pins"), SerializeField] private RectTransform pinUp;
		[Foldout("Pins"), SerializeField] private RectTransform pinSide;
		[Foldout("Pins"), SerializeField] private RectTransform pinDown;
		#endregion
	
		protected Transform cachedTransform;
		private Tween resetTween;
	
		private const float MARGIN_X = 102f;
		private const float MARGIN_Y = 102.6f;
	
		public bool IsPanelActive => gameObject.activeSelf;

		private void Awake()
		{
			SetTextPreferredWidth();
		}

		public void Show(RectTransform targetTransform, string message, Position position,
			Vector3? referencePosition = null, Transform parent = null, CloseOption closeOption = CloseOption.Input,
			float? displayTime = null)
		{
			if (parent)
			{
				transform.SetParent(parent);
			}
			AlignPanel(targetTransform, position, referencePosition, closeOption);
			SetMessageText(message);

			if (DoesCloseOptionMatch(closeOption,CloseOption.Time) && displayTime.HasValue)
			{
				SetDelayedResetCallback(displayTime.Value);
			}
		}

		protected void SetDelayedResetCallback(float delay = 2f)
		{
			resetTween = DOVirtual.DelayedCall(delay, () => ResetPanel(true, false));
		}
	
		public virtual void Hide(bool withAnimation = false)
		{
			if (resetTween != null)
			{
				resetTween.Kill();
				resetTween = null;
			}
			_ = SetState(false, withAnimation);
		}
	
		protected void ResetPanel(bool withAnimation = false, bool disableInputHandler = true)
		{
			if (disableInputHandler)
			{
				DisableInputHandlers();
			}
			
			cachedTransform = null;
			
			if (gameObject.activeInHierarchy)
			{
				Hide(withAnimation);
			}
			else
			{
				resetTween = null;
			}
		}
	
		protected bool AlignPanel(RectTransform targetTransform, Position position, Vector3? referencePosition = null, 
			CloseOption closeOption = CloseOption.Input)
		{
			if (targetTransform != cachedTransform)
			{
				SetCloseOption(targetTransform,closeOption);
				switch (position)
				{
					case Position.AutoHorizontal:
					case Position.Left:
					case Position.Right:
						AlignHorizontally(targetTransform, position, referencePosition);
						break;
					case Position.AutoVertical:
					case Position.Above:
					case Position.Below:
						AlignVertically(targetTransform, position, referencePosition);
						break;
				}
				return true;
			}
			else
			{
				DisablePanel();
				cachedTransform = null;
				return false;
			}
		}

		protected virtual void SetCloseOption(RectTransform targetTransform,CloseOption closeOption = CloseOption.Input)
		{
			if (DoesCloseOptionMatch(closeOption,CloseOption.Input))
			{
				/*
				InputHandler.instance.EnableInputDown(_ => Hide(), true);
				InputHandler.instance.EnableInputUp(_ =>
				{
					if (targetTransform == cachedTransform)
					{
						cachedTransform = null;
					}
				}, true);
				*/
			}
		}
	
		private void AlignHorizontally(RectTransform targetTransform, Position position, 
			Vector3? referencePosition = null)
		{
			Vector2 pivotPosition = targetTransform.position;
			
			if (position == Position.AutoHorizontal)
			{
				position = GetPosition();
			}
			SetPinState(position);
			Vector2 pivot = GetPivot();
			float anchor = 0.5f;
			float pinPosition = 0;
			panel.pivot = pivot;
			panel.transform.position = pivotPosition;
			float xPosition = (position == Position.Left ? -1f : 1f) *
			                  (((targetTransform.rect.width * targetTransform.localScale.x) / 2f) + GetPinOffset());
            
			panel.anchoredPosition += new Vector2(xPosition, 0f);

			if (!Mathf.Approximately(pivot.y,0.5f))
			{
				panel.anchoredPosition += Vector2.up * (GetMargin(position) * (pivot.y > 0 ? 1f : -1f));
				anchor = pivot.y > 0 ? 1 : 0;
				pinPosition = GetMargin(position) * (pivot.y > 0 ? -1f : 1f);
			}
			LocatePin(position, anchor, pinPosition);
			_ = SetState(true);
            
			cachedTransform = targetTransform;
			
			float GetPinOffset() => pinSide.sizeDelta.x * 0.23f;

			Position GetPosition()
			{
				return referencePosition.HasValue
					? targetTransform.position.x > referencePosition.Value.x
						? Position.Left
						: Position.Right
					: Position.Right;
			}

			Vector2 GetPivot()
			{
				Vector2 calculatedPivot = Vector2.one * 0.5f;
				calculatedPivot.x = position == Position.Left ? 1f : 0f;
				if (Mathf.Abs(targetTransform.position.y) > 3f)
				{
					calculatedPivot.y = targetTransform.position.y > 0 ? 1f : 0f;
				}
				return calculatedPivot;
			}
		}
    
		private void AlignVertically(RectTransform targetTransform, Position position,
			Vector3? referencePosition = null)
		{
			Vector2 pivotPosition = targetTransform.position;

			if (position == Position.AutoVertical)
			{
				position = GetPosition();
			}
			SetPinState(position);
			Vector2 pivot = GetPivot();
			float anchor = 0.5f;
			float pinPosition = 0;
			panel.pivot = pivot;
			panel.transform.position = pivotPosition;
			float yPosition = (position == Position.Below ? -1f : 1f) *
			                  (((targetTransform.rect.height * targetTransform.localScale.y) / 2f) + GetPinOffset());
            
			panel.anchoredPosition += new Vector2(0f, yPosition);

			if (!Mathf.Approximately(pivot.x,0.5f))
			{
				panel.anchoredPosition += Vector2.right * (GetMargin(position) * (pivot.x > 0 ? 1f : -1f));
				anchor = pivot.x > 0 ? 1 : 0;
				pinPosition = GetMargin(position) * (pivot.x > 0 ? -1f : 1f);
			}
			LocatePin(position, anchor, pinPosition);
			_ = SetState(true);
            
			cachedTransform = targetTransform;

			float GetPinOffset() => position == Position.Below ? pinUp.sizeDelta.y * 0.42f : pinDown.sizeDelta.y * 0.2f;

			Position GetPosition()
			{

				return referencePosition.HasValue
					? targetTransform.position.y > referencePosition.Value.y
						? Position.Below
						: Position.Above
					: Position.Below;
			}

			Vector2 GetPivot()
			{
				Vector2 calculatedPivot = Vector2.one * 0.5f;
				if (Mathf.Abs(targetTransform.position.x) > 1.8f)
				{
					calculatedPivot.x = targetTransform.position.x > 0 ? 1f : 0f;
				}
				calculatedPivot.y = position == Position.Below ? 1f : 0f;
				return calculatedPivot;
			}
		}

		private float GetMargin(Position position)
		{
			switch (position)
			{
				case Position.Below:
				case Position.Above:
					return MARGIN_X;
				case Position.Left:
				case Position.Right:
					return MARGIN_Y;
			}
			return 0f;
		}

		protected virtual async UniTask SetState(bool isActive, bool withAnimation = true, float animationDuration = 0.25f)
		{
			panel.DOKill(true);

			if (isActive)
			{
				panel.gameObject.SetActive(true);
				if (withAnimation)
				{
					panel.localScale = Vector2.zero;
					_ = panel.DOScale(Vector2.one, animationDuration).SetEase(Ease.OutBack);
				}
			}
			else
			{
				if (withAnimation)
				{
					DisableInputHandlers();
					await panel.DOScale(0f, animationDuration/2f).SetEase(Ease.InBack).OnComplete(DisablePanel);
				}
				else
				{
					DisablePanel();
				}
			}
		}
		protected void DisableInputHandlers()
		{
			/*
			InputHandler.instance.DisableInputDown();
			InputHandler.instance.DisableInputUp();
			*/
		}
		protected virtual void DisablePanel()
		{
			this.ResetObject();
		}
    
		#region Panel Pin Operations

		private void SetPinState(Position panelPosition)
		{
			pinUp.gameObject.SetActive(panelPosition == Position.Below);
			pinDown.gameObject.SetActive(panelPosition == Position.Above);
			pinSide.gameObject.SetActive(panelPosition == Position.Right || panelPosition == Position.Left);
		}

		private void LocatePin(Position panelPosition, float anchor, float pinPosition)
		{
			switch (panelPosition)
			{
				case Position.Below:
					pinUp.anchorMin = new Vector2(anchor, 1f);
					pinUp.anchorMax = new Vector2(anchor, 1f);
					pinUp.anchoredPosition = new Vector2(pinPosition, pinUp.anchoredPosition.y);
					break;
				case Position.Above:
					pinDown.anchorMin = new Vector2(anchor, 0f);
					pinDown.anchorMax = new Vector2(anchor, 0f);
					pinDown.anchoredPosition = new Vector2(pinPosition, pinDown.anchoredPosition.y);
					break;
				case Position.Right:
				{
					float cachedPosX = pinSide.anchoredPosition.x;
					pinSide.anchorMin = new Vector2(0f, anchor);
					pinSide.anchorMax = new Vector2(0f, anchor);
					pinSide.anchoredPosition = new Vector2(Mathf.Abs(cachedPosX), pinPosition);
					pinSide.localScale = new Vector2(-1f, 1f);
					break;
				}
				case Position.Left:
				{
					float cachedPosX = pinSide.anchoredPosition.x;
					pinSide.anchorMin = new Vector2(1f, anchor);
					pinSide.anchorMax = new Vector2(1f, anchor);
					pinSide.anchoredPosition = new Vector2(Mathf.Abs(cachedPosX) * -1f, pinPosition);
					pinSide.localScale = Vector2.one;
					break;
				}
			}
		}
    
		#endregion

		protected void SetMessageText(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				messageText.gameObject.SetActive(true);
				messageText.text = text;
			}
			else
			{
				messageText.gameObject.SetActive(false);
			}
		}

		private void SetTextPreferredWidth()
		{
			messageTextLayoutElement.preferredWidth = Display.main.systemWidth / 2f;
		}
		
		#region Pooling

		public ObjectType ObjectType { get; set; }
		public virtual void OnSpawn() { }

		public virtual void OnReset() { }
        
		#endregion
	}
}