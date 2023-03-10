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
        [SerializeField] private float _HeavyShootingSpeed;
        
        private int _ammoCount = 0;
        private int _shootingSpeedModif = 60;
        private bool _canShoot = false;
        private bool _canHeavyShoot = false;
        private bool _canShootLocal = true;
        private bool _canShootHeavyLocal = true;

        public bool SetCanShoot
        {
            set => _canShoot = value;
        }

        public bool SetCanHeavyShoot
        {
            set => _canHeavyShoot = value;
        }

        private void LateUpdate()
        {
            ApplyShoot();
            ApplyHeavyShoot();
        }

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

        private void ApplyHeavyShoot()
        {
            if (_canHeavyShoot && _canShootHeavyLocal)
            {
                _sparks.gameObject.SetActive(true);
                _bulletList[_bulletList.Count-1].gameObject.SetActive(true);
                StartCoroutine(OnHeavyShootRutine());
            }
        }

        private IEnumerator OnShootRutine()
        {
            _canShootLocal = false;
            if (_ammoCount == 5) _ammoCount = 0;

            while (true)
            {
                yield return new WaitForSecondsRealtime(_shootingSpeedModif / _shootingSpeed);
                _sparks.gameObject.SetActive(false);
                _canShootLocal = true;
                yield break;
            }
        }
        private IEnumerator OnHeavyShootRutine()
        {
            _canShootHeavyLocal = false;

            while (true)
            {
                yield return new WaitForSecondsRealtime(_shootingSpeedModif / _shootingSpeed);
                _sparks.gameObject.SetActive(false);
                yield return new WaitForSecondsRealtime(_shootingSpeedModif / _HeavyShootingSpeed);
                _canShootHeavyLocal = true;
                yield break;
            }
        }
    }
}