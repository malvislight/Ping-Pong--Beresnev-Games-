using System;
using UI.Screens;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        [Range(5, 10)] [SerializeField] private float _minSpeed = 6;
        [Range(11, 15)] [SerializeField] private float _maxSpeed = 15;

        private float _speed;
        private Vector2 _direction;
        private Action _onStart;
        protected Action OnGoal;
        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            SetRandomSpeed();
            _onStart = () => { SetDirection(GetRandomDirection()); };
            OnGoal = () =>
            {
                SetRandomSpeed();
                SetDirection(Vector2.zero);
            };
        }

        protected static Vector2 GetRandomDirection()
        {
            return Random.insideUnitCircle + Vector2.up * (Random.Range(0, 2) * 2 - 1);
        }
        protected void SetRandomSpeed()
        {
            _speed = Random.Range(_minSpeed, _maxSpeed);
        }

        private void OnEnable()
        {
            StartScreen.OnStart += _onStart;
            BallCollisionDetector.OnGoal += OnGoal;
        }

        protected void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized * _speed;
            _rigidbody.velocity = _direction;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                SetDirection(transform.position - other.transform.position);
                return;
            }
            
            _direction.x *= -1;
            SetDirection(_direction);
        }

        private void OnDisable()
        {
            StartScreen.OnStart -= _onStart;
            BallCollisionDetector.OnGoal -= OnGoal;
        }
    }   
}