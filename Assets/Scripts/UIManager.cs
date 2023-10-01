using DG.Tweening;
using System;
using UnityEngine;

namespace LD54
{
	public class UIManager : MonoBehaviour
	{
		[Header("Manager")]
		[SerializeField]
		UpgradesUIManager _upgradesUIManager;

		[Space(3.0f)]

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
		private RectTransform _collectTooltipPanel;
		[SerializeField]
		private RectTransform _turretUpgradePanel;

		[Space(2.0f)]

		[Header("Animation Timings")]
		[SerializeField]
		private float _collectTooltipMoveTime = 0.5f;
		[SerializeField]
		private float _upgradePanelMoveTime = 0.5f;


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
					HideUpgradePanel();
					HideGamePanel();
					break;

				case GameState.Wave:
					HideMainMenuPanel();
					HideUpgradePanel();
					ShowGamePanel();
					break;

				case GameState.Collect:
					HideMainMenuPanel();
					HideUpgradePanel();
					ShowCollectTooltipPanel();
					break;

				case GameState.Upgrade:
					HideMainMenuPanel();
					HideCollectTooltipPanel();
					ShowUpgradePanel();					
					break;

				case GameState.Win:
					HideMainMenuPanel();
					HideUpgradePanel();

					break;

				case GameState.Lose:
					HideMainMenuPanel();
					HideUpgradePanel();
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

		private void ShowUpgradePanel()
		{
			_turretUpgradePanel.gameObject.SetActive(true);
			_turretUpgradePanel.DOScale(Vector3.one, _upgradePanelMoveTime).SetEase(Ease.InOutQuint);
		}

		private void HideUpgradePanel()
		{
			_turretUpgradePanel.DOKill();
			_turretUpgradePanel.DOScale(Vector3.zero, _upgradePanelMoveTime).SetEase(Ease.InOutQuint)
				.OnComplete(() => { _turretUpgradePanel.gameObject.SetActive(false); });
		}

		public void ShowCollectTooltipPanel()
		{
			_collectTooltipPanel.DOKill();
			_collectTooltipPanel.gameObject.SetActive(true);
			_collectTooltipPanel.DOAnchorPos(new Vector2(0, 20), _collectTooltipMoveTime).SetEase(Ease.InOutQuint);
		}

		public void HideCollectTooltipPanel()
		{
			_collectTooltipPanel.DOKill();
			_collectTooltipPanel.DOAnchorPos(new Vector2(0, -115), _collectTooltipMoveTime).SetEase(Ease.InOutQuint)
				.OnComplete(() => { _collectTooltipPanel.gameObject.SetActive(false); });

		}

		public void ShowWinPanel()
		{
			_winPanel.SetActive(true);
		}

		public void HideWinPanel()
		{
			_winPanel.SetActive(false);
		}

		public void ShowLosePanel()
		{
			_losePanel.SetActive(true);

		}

		public void HideLosePanel()
		{
			_losePanel.SetActive(false);
		}
	} 
}
