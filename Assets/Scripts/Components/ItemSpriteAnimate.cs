using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Fantasie
{
    public class ItemSpriteAnimate : MonoBehaviour
    {
        [SerializeField] private float _startScale = .95f;
        [SerializeField] private float _endScale = 1.05f;
        [SerializeField] private float _scaleDuration = .6f;

        private void Start()
        {
            StartCoroutine(StartLifeAnimationRutine());
        }
        private IEnumerator StartLifeAnimationRutine()
        {
            while (true)
            {
                DOTween.Sequence()
                    .Append(transform.DOScale(_startScale, _scaleDuration))
                    .Append(transform.DOScale(_endScale, _scaleDuration));
                yield return new WaitForSeconds(1f);
            }
        }
    }
}