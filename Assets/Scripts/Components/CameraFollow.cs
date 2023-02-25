using UnityEngine;

namespace Fantasie
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _damping;

        private float _offset = 1f;

        private void LateUpdate()
        {
            var destination = new Vector3(_target.position.x, _target.position.y + _offset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _damping);
        }
    }
}