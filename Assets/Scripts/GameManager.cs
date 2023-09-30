using System;
using UnityEngine;

namespace LD54
{
    public class GameManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField]
        private WaveManager _waveManager;
        [SerializeField]
        private UIManager _uIManager;

        [Header("Game")]
        [SerializeField]
        private Turret _turret;
        private GameState _state;
        private int _currentWaveNumber = -1;

        public Action<GameState> OnGameStateChanged;

        public GameState CurrentGameState { get => _state; set { OnGameStateChanged?.Invoke(value); } }
        public Turret Turret              { get => _turret; set => _turret = value; }
        public UIManager UIManager        { get => _uIManager; set => _uIManager = value; }
        public WaveManager WaveManager    { get => _waveManager; set => _waveManager = value; }

        private void Awake()
        {
            CurrentGameState = GameState.Menu;
            _uIManager.ShowMainMenuPanel();
        }

        private void OnEnable()
        {
            OnGameStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            OnGameStateChanged -= OnStateChanged;
        }

        private void OnStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Menu:
                    break;
                case GameState.Wave:
                    _turret.CanShoot = true;
                    _currentWaveNumber++;
                    _waveManager.BeginWave(_currentWaveNumber);
                    break;
                case GameState.Collect:
                    break;
                case GameState.Upgrade:
                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;
            }
        }

        public void StartGame()
        {
            CurrentGameState = GameState.Wave;
        }
    } 
}
