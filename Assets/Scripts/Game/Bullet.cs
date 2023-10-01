using UnityEngine;

namespace LD54
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Launch(Vector3 direction)
        {
            _rb.velocity = direction;
        }

        private void FixedUpdate()
        {
            if (!_spriteRenderer.isVisible)
                Destroy(gameObject);
        }
    } 
}
