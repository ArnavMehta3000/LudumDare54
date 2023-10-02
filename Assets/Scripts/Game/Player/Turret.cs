
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
        private AnimationCurve _shootingSpeedCurve;
        private float _currentShootingSpeed = 1.0f;
        [SerializeField]
        private AnimationCurve _rotationSpeedCurve;
        private float _currentRotationSpeed = 100.0f;
        [SerializeField]
        private AnimationCurve _bulletLaunchSpeedCurve;
        private float _currentBulletLaunchForce = 1.0f;
        [SerializeField]
        private AnimationCurve _attractionRadiusCurve;
        private float _currentAttractionRadius = 15.0f;

        [SerializeField]
        private Transform _shootPoint;
        private float _currentShootTime = 0.0f;

        [Space(3.0f)]

        [Header("UI")]
        [SerializeField]
        private float _tooltipMoveTime = 0.5f;
        [SerializeField]
        private RectTransform _helperTooltipPanel;

        private int _shootSpeedIndex, _rotationSpeedIndex, _bulletLaunchSpeedIndex, _attractionRadiusIndex, _multiShootIndex;

        public bool CanShoot           { get; set; } = false;
        public float ShootingSpeed     { get => _currentShootingSpeed;            set => _currentShootingSpeed = value; }
        public float BulletLaunchForce { get => _currentBulletLaunchForce;        set => _currentBulletLaunchForce = value; }
        public float AttractionRadius  { get => _currentAttractionRadius; private set => _currentAttractionRadius = value; }

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            EvaluateCurrent();
            _multiShootIndex = 1;
        }

        private void EvaluateCurrent()
        {
            _currentShootingSpeed = _shootingSpeedCurve.Evaluate(_shootSpeedIndex);
            _currentRotationSpeed = _rotationSpeedCurve.Evaluate(_rotationSpeedIndex);
            _currentBulletLaunchForce = _bulletLaunchSpeedCurve.Evaluate(_bulletLaunchSpeedIndex);
            _currentAttractionRadius = _attractionRadiusCurve.Evaluate(_attractionRadiusIndex);

            SetAttractionRadius(_currentAttractionRadius);
        }

        private void Update()
        {
            if (!CanShoot)
                return;

            Rotate();

            _currentShootTime += Time.deltaTime;
            if (_currentShootTime > _currentShootingSpeed)
            {
                _currentShootTime = 0.0f;
                Shoot();
            }
        }

        private void Rotate()
        {
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            {
                transform.Rotate(0, 0, _currentRotationSpeed * Time.deltaTime);
            }

            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                transform.Rotate(0, 0, -_currentRotationSpeed * Time.deltaTime);
            }
        }

        private void Shoot()
        {
            Bullet bullet = Instantiate(_bulletPrefab, _shootPoint.position, transform.rotation).GetComponent<Bullet>();
            bullet.Launch(transform.up * _currentBulletLaunchForce);
        }

        public void SetAttractionRadius(float radius)
        {
            _currentAttractionRadius = radius;
            gameObject.DrawCircle(_currentAttractionRadius, 0.1f, Color.white);
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
                    _helperTooltipPanel.DOKill();
                    _helperTooltipPanel.DOAnchorPos(new Vector2(0, 115), _tooltipMoveTime).SetEase(Ease.InOutQuint)
                        .OnComplete(() => { _helperTooltipPanel.gameObject.SetActive(false); });
                    break;
            }
        }

        public void IncreaseBulletSpeed()
        {
            _bulletLaunchSpeedIndex++;
            EvaluateCurrent();
        }

        public void IncreaseBulletSpawnRate()
        {
            _shootSpeedIndex++;
            EvaluateCurrent();
        }

        public void DecreaseAttractionRadius()
        {
            _attractionRadiusIndex++;
            EvaluateCurrent();
        }

        public void IncreaseMultiBullets()
        {
            _multiShootIndex++;
            EvaluateCurrent();
        }
    } 
}
