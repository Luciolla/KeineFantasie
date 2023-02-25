using UnityEngine;

namespace Fantasie
{
    public class StaffAiming : MonoBehaviour
    {
        [SerializeField] private GameObject _staff;

        private Camera _camera;
        private Quaternion _rotation;

        public Quaternion GetRotation => _rotation;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            OnAimMotion();
        }

        private void OnAimMotion()
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;

            _rotation = Quaternion.Euler(0f, 0f, angle);
            _staff.transform.rotation = _rotation;
        }
    }
}