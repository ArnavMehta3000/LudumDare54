using UnityEngine;

namespace LD54
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rb;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Launch(Vector3 direction)
        {
            _rb.velocity = direction;
            Destroy(gameObject, 5.0f);
        }
    } 
}
