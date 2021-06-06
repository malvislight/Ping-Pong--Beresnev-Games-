using Player.Data;
using UnityEngine;

namespace Ball
{
    public class BallColorSetter : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            Set();
        }

        private void OnEnable()
        {
            BallColor.OnChanged += Set;
        }
        
        private void OnDisable()
        {
            BallColor.OnChanged -= Set;
        }

        private void Set()
        {
            _spriteRenderer.color = BallColor.Color;
        }
        
    }
}