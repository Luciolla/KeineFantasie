using UnityEngine;

namespace Fantasie
{
    public class CheckLayer : MonoBehaviour
    {
        public bool IsTouchingLayer;

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Collider2D _collider;

        private void OnTriggerStay2D(Collider2D collision)
        {
            IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);
        }
    }
}