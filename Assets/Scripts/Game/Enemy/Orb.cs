using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LD54
{
    [RequireComponent (typeof (Rigidbody2D))]
    public class Orb : MonoBehaviour
    {
        [SerializeField]
        private float _nudgeForce = 10.0f;
        [SerializeField]
        private SpriteRenderer _renderer;

        private GameManager _gameManager;
        private Rigidbody2D _rb;
        private CircleCollider2D _circleCollider;
        private bool _isCollected = false;


        public void OnMouseEnter()
        {
            if (_gameManager.CurrentGameState != GameState.Collect && !_isCollected)
                return;
            
            _isCollected = true;
            _circleCollider.enabled = false;
            _renderer.DOFade(0.0f, 0.25f).SetEase(Ease.OutQuint).OnComplete(() => { Destroy(gameObject); });
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _gameManager = FindObjectOfType<GameManager>();
            _circleCollider = GetComponent<CircleCollider2D>();

        }

        private void Start()
        {
            _rb.AddForce(new Vector2(Random.Range(-_nudgeForce, _nudgeForce), Random.Range(-_nudgeForce, _nudgeForce)));
        }
    } 
}
