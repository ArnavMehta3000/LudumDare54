using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1.0f;
    private Rigidbody2D _rb;
    private Vector3 _turretPosition;
    private float _turretAttractionRadius;
    private bool _turretAsTarget = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Vector3 turretPosition, float turretAttractionRadius, Vector3 targetPosition)
    {
        _turretAttractionRadius = turretAttractionRadius;
        _turretPosition = turretPosition;

        Vector3 direction = (targetPosition - transform.position).normalized;
        _rb.velocity = direction * _moveSpeed;
        Debug.DrawLine(transform.position, targetPosition, Color.cyan, 5.0f);
    }

    private void Update()
    {
        if (!_turretAsTarget && Vector3.Distance(transform.position, _turretPosition) < _turretAttractionRadius)
        {
            _turretAsTarget = true;
            SetTarget(_turretPosition, _turretAttractionRadius, _turretPosition);
        }
    }
}
