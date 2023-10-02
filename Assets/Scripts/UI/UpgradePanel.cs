using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LD54
{
	public class UpgradePanel : MonoBehaviour
	{
		[SerializeField]
		private Image[] _levels;
		private int _currentLevel = -1;
		public UnityEvent OnBuyButtonPressed;


		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void CallButtonCallback()
		{
			// Increase level
			_currentLevel++;
			SetLevel();
			OnBuyButtonPressed?.Invoke();
		}

		private void SetLevel()
		{
			// Set UI level bars
			_currentLevel++;
			_currentLevel = _currentLevel > 3 ? 3 : _currentLevel;
			for (int i = 0; i < _currentLevel; i++)
			{
				_levels[i].color = Color.yellow;
			}

		}
	} 
}
