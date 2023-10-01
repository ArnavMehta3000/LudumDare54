
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LD54
{
    public class Turret : MonoBehaviour
    {
        private GameManager _gameManager;

        [Header("Shooting")]
        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private float _shootingSpeed = 1.0f;
        [SerializeField]
        private float _rotationSpeed = 100.0f;
        [SerializeField]
        private float _bulletLaunchForce = 1.0f;
        [SerializeField]
        private Transform _shootPoint;
        [SerializeField]
        private float _attractionRadius = 15.0f;
        private float _currentShootTime = 0.0f;

        [Space(3.0f)]

        [Header("UI")]
        [SerializeField]
        private float _tooltipMoveTime = 0.5f;
        [SerializeField]
        private RectTransform _helperTooltipPanel;


        private bool IsUpgrading       { get; set; } = false;
        public bool CanShoot           { get; set; } = false;
        public float ShootingSpeed     { get => _shootingSpeed;            set => _shootingSpeed = value; }
        public float BulletLaunchForce { get => _bulletLaunchForce;        set => _bulletLaunchForce = value; }
        public float AttractionRadius  { get => _attractionRadius; private set => _attractionRadius = value; }

        private void Start()
        {
            SetAttractionRadius(_attractionRadius);
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (!CanShoot)
                return;

            Rotate();

            _currentShootTime += Time.deltaTime;
            if (_currentShootTime > _shootingSpeed)
            {
                _currentShootTime = 0.0f;
                Shoot();
            }
        }

        private void Rotate()
        {
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
            }

            if (Keyboard.current.rightArrowKey.isPressed)
            {
                transform.Rotate(0, 0, -_rotationSpeed * Time.deltaTime);
            }
        }

        private void Shoot()
        {
            Bullet bullet = Instantiate(_bulletPrefab, _shootPoint.position, transform.rotation).GetComponent<Bullet>();
            bullet.Launch(transform.up * _bulletLaunchForce);
        }

        public void SetAttractionRadius(float radius)
        {
            _attractionRadius = radius;
            gameObject.DrawCircle(_attractionRadius, 0.1f, Color.white);
        }

        private void OnMouseEnter()
        {
            switch (_gameManager.CurrentGameState)
            {
                case GameState.Wave:
                case GameState.Collect:
                    // Show helper tooltip
                    _helperTooltipPanel.gameObject.SetActive(true);
                    _helperTooltipPanel.DOKill();
                    _helperTooltipPanel.DOAnchorPos(new Vector2(0, -20), _tooltipMoveTime).SetEase(Ease.InOutQuint);
                    break;
            }
        }

        private void OnMouseExit()
        {
            switch (_gameManager.CurrentGameState)
            {
                case GameState.Wave:
                case GameState.Collect:
                    // Hide helper tooltip
                    _helperTooltipPanel.gameObject.SetActive(false);
                    _helperTooltipPanel.DOKill();
                    _helperTooltipPanel.DOAnchorPos(new Vector2(0, 115), _tooltipMoveTime).SetEase(Ease.InOutQuint);
                    break;
            }
        }
    } 
}
