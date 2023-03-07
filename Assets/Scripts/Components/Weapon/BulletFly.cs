using System.Collections;
using UnityEngine;

namespace Fantasie
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class BulletFly : MonoBehaviour
    {
        //todo take projectile stats from SO
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Rigidbody2D _bulletBody;
        [SerializeField] private GameObject _firePoint;
        [SerializeField] private Animator _animator;

        private float _lifeTime;
        private float _shootingForce;

        private void OnEnable()
        {
            GetData();
            ResetTransform();
            StartCoroutine(FlyBulletRutine());
        }

        #region ResetTransform
        private void ResetTransform()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.SetParent(null);
        }

        private void ResetTransform(Transform value)
        {
            _bullet.gameObject.SetActive(false);
            transform.SetParent(value);
            transform.localPosition = Vector3.zero;
            transform.localRotation = value.localRotation;
        }
        #endregion

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision != null)
            {
                ResetTransform(_firePoint.transform);
            }
        }

        private void GetData()
        {
            _animator.runtimeAnimatorController = _weaponData.GetProjectileAnimation;
            _lifeTime = _weaponData.GetProjectileLifeTime;
            _shootingForce = _weaponData.GetWeaponShootForce;
        }

        private IEnumerator FlyBulletRutine()
        {
            while (true)
            {
                _bulletBody.AddForce(transform.up * _shootingForce, ForceMode2D.Impulse);
                yield return new WaitForSecondsRealtime(_lifeTime);
                ResetTransform(_firePoint.transform);
                yield break;
            }
        }
    }
}