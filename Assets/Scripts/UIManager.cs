using DG.Tweening;
using UnityEngine;

namespace LD54
{
	public class UIManager : MonoBehaviour
	{
		[Header("Panels")]
		[SerializeField]
		private GameObject _mainMenuPanel;
		[SerializeField]
		private GameObject _gamePanel;
		[SerializeField]
		private GameObject _winPanel;
		[SerializeField]
		private GameObject _losePanel;
		[SerializeField]
		private RectTransform _collectTooltopPanel;

		[Space(2.0f)]

		[Header("Animation Timings")]
		[SerializeField]
		private float _collectTooltipMoveTime = 0.5f;


		private void OnEnable()
		{
			var gm = FindObjectOfType<GameManager>();
			if (gm)
			{
				gm.OnGameStateChanged += OnGameStateChanged; 
			}
		}

		private void OnDisable()
		{
			var gm = FindObjectOfType<GameManager>();
			if (gm)
			{
				gm.OnGameStateChanged -= OnGameStateChanged;
			}
		}

		private void OnGameStateChanged(GameState newState)
		{
			switch (newState)
			{
				case GameState.Menu:
					ShowMainMenuPanel();
					HideGamePanel();
					break;

				case GameState.Wave:
					HideMainMenuPanel();
					ShowGamePanel();
					break;

				case GameState.Collect:
					HideMainMenuPanel();
					ShowCollectTooltipPanel();
					break;

				case GameState.Upgrade:
					HideMainMenuPanel();
					HideCollectTooltipPanel();
					break;

				case GameState.Win:
					HideMainMenuPanel();
					break;

				case GameState.Lose:
					HideMainMenuPanel();
					break;
			}
		}


		public void ShowMainMenuPanel()
		{
			_mainMenuPanel.SetActive(true);
		}

		public void HideMainMenuPanel()
		{
			_mainMenuPanel.SetActive(false);
		}

		public void ShowGamePanel()
		{
			_gamePanel.SetActive(true);
		}

		public void HideGamePanel()
		{
			_gamePanel.SetActive(false);
		}

		public void ShowCollectTooltipPanel()
		{
			_collectTooltopPanel.DOAnchorPos(new Vector2(0, 20),_collectTooltipMoveTime).SetEase(Ease.InOutQuint);
		}

		public void HideCollectTooltipPanel()
		{
			_collectTooltopPanel.DOAnchorPos(new Vector2(0, -40),_collectTooltipMoveTime).SetEase(Ease.InOutQuint);

		}
	} 
}
