using UnityEngine;

namespace LD54
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private GameObject _mainMenuPanel;
		[SerializeField]
		private GameObject _gamePanel;
		[SerializeField]
		private GameObject _winPanel;
		[SerializeField]
		private GameObject _losePanel;

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
					break;

				case GameState.Upgrade:
					HideMainMenuPanel();
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
	} 
}
