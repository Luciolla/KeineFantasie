using UnityEngine;

namespace Fantasie
{
    public class EnemyAiming : MonoBehaviour
    {
        [SerializeField] private GameObject _staff = null;
        [SerializeField] private GameObject _target = null;

        private Quaternion _rotation;

        public GameObject SetTarget
        {
            get => _target;
            set => _target = value;
        }

        private void FixedUpdate() => OnAimMotion();

        private void OnAimMotion()
        {
            if (_target == null) return;
            if (_staff == null) return;

            var angle = Mathf.Atan2(_target.transform.position.y - transform.position.y, _target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

            _rotation = Quaternion.Euler(0f, 0f, angle);
            _staff.transform.rotation = _rotation;
        }
    }
}