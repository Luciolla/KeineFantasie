using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fantasie
{
    public class ShootWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject _sparks;
        [SerializeField] private List<GameObject> _bulletList;
        [Header("Shoots per minute")]
        [SerializeField] private float _shootingSpeed;

        private int _ammoCount = 0;
        private int _shootingSpeedModif = 60;
        private bool _canShoot = false;
        private bool _canShootLocal = true;

        public bool SetCanShoot
        {
            set => _canShoot = value;
        }

        private void Update() => Debug.Log(_canShoot);
        private void LateUpdate() => ApplyShoot();

        private void ApplyShoot()
        {
            if (_ammoCount == 5) _ammoCount = 0;

            if (_canShoot && _canShootLocal)
            {
                _sparks.gameObject.SetActive(true);
                _bulletList[_ammoCount].gameObject.SetActive(true);
                _ammoCount++;
                StartCoroutine(OnShootRutine());
            }
        }

        private IEnumerator OnShootRutine()
        {
            _canShootLocal = false;
            if (_ammoCount == 5) _ammoCount = 0;
            _ammoCount++;

            while (true)
            {
                yield return new WaitForSecondsRealtime(_shootingSpeedModif/_shootingSpeed);
                _sparks.gameObject.SetActive(false);
                _canShootLocal = true;
                yield break;
            }
        }
    }
}