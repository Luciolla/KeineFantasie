using System.Collections.Generic;
using UnityEngine;

namespace Fantasie
{
    public class Ultimate : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _bulletList;
        [SerializeField] private UltimateEnergy _energy;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private bool _canUltimateShoot = false;

        public bool SetCanUltimateShoot
        {
            set => _canUltimateShoot = value;
        }

        private void Update()
        {
            if (_canUltimateShoot) ApplyUltimate();
        }

        private void ApplyUltimate()
        {
            if (_energy.IsEnergyEnough)
            {
                foreach (var item in _bulletList)
                {
                    item.SetActive(true);
                    _audioSource.PlayOneShot(_audioClip);
                    _energy.GetEnergy = 0f;
                }
            }
            else
            {
                Debug.Log("NotEnoughEnergy");
            }
        }
    }
}