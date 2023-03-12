using System.Collections;
using UnityEngine;

namespace Fantasie
{
    public class StepsSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        private bool _isTrue = false;

        public bool IsMoving
        {
            set => _isTrue = value;
        }

        private void Start() => StartCoroutine(PlaySoundWhenSignalIsTrue());

        private IEnumerator PlaySoundWhenSignalIsTrue()
        {
            while (true)
            {
                if(_isTrue) _source.Play();
                yield return new WaitUntil(() =>_isTrue == false);
                _source.Stop();
            }
        }
    }
}