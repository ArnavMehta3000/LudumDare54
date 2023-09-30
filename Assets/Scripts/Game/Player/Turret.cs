
using UnityEngine;
using UnityEngine.InputSystem;

namespace LD54
{
    public class Turret : MonoBehaviour
    {
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

        public LineRenderer _lineRenderer;

        public bool CanShoot = false;
        public float ShootingSpeed { get => _shootingSpeed; set => _shootingSpeed = value; }
        public float BulletLaunchForce { get => _bulletLaunchForce; set => _bulletLaunchForce = value; }
        public float AttractionRadius { get => _attractionRadius; private set => _attractionRadius = value; }

        private void Start()
        {
            SetAttractionRadius(_attractionRadius);
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
    } 
}
