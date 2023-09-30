using UnityEngine;

namespace LD54
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBase : MonoBehaviour
    {
        [Header("Base Movement")]
        [SerializeField]
        private float _moveSpeed = 1.0f;
        [SerializeField]
        private float _speedMultiplier = 1.0f;

        [Header("Enemy Specifics")]
        private int _health = 1;

        private Rigidbody2D _rb;
        private Vector3 _turretPosition;
        private float _turretAttractionRadius;
        private bool _turretAsTarget = false;
        private WaveManager _waveManager;


        public float SpeedMultiplier { get => _speedMultiplier; set => _speedMultiplier = value; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Used for initial setup
        public void Launch(Vector3 turretPosition, float turretAttractionRadius, Vector3 targetPosition, WaveManager manager)
        {
            _waveManager = manager;
            _turretAttractionRadius = turretAttractionRadius;
            _turretPosition = turretPosition;

            Vector3 direction = (targetPosition - transform.position).normalized;
            _rb.velocity = direction * _moveSpeed;
            Debug.DrawLine(transform.position, targetPosition, Color.cyan, 5.0f);
        }

        // Used for setting attraction speed
        public void SetTarget(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            _rb.velocity = direction * _moveSpeed * _speedMultiplier;
        }

        private void Update()
        {
            if (!_turretAsTarget && Vector3.Distance(transform.position, _turretPosition) < _turretAttractionRadius)
            {
                _turretAsTarget = true;
                SetTarget(_turretPosition);
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Collided with bullet
            if(collision.GetComponent<Bullet>() != null)
            {
                _health--;
                if (_health <= 0)
                    _waveManager.RemoveEnemy(this);
            }
            
            // Collided with player turret
            if (collision.GetComponent<Turret>() != null)
            {
                Debug.Log("Player hit!");
            }
        }
    } 
}
