using System.Collections;
using UnityEngine;

namespace Fantasie
{
    public class UpdateSpriteDirection : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _renderer;

        private float _time = .1f;

        private void OnEnable()
        {
            StartCoroutine(UpdateSprite());
        }
        private IEnumerator UpdateSprite()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(_time);
                if (_rigidbody2D.velocity.x > 0)
                {
                    _renderer.flipX = false;
                }
                else if (_rigidbody2D.velocity.x < 0)
                {
                    _renderer.flipX = true;
                }
            }
        }
    }
}