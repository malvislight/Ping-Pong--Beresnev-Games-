using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }
        private void Update()
        {
            if(Input.GetMouseButton(0) == false) return;
            
            transform.position = Vector3.up * transform.position.y + Vector3.right * _camera.ScreenToWorldPoint(Input.mousePosition).x;
        }
    }
}