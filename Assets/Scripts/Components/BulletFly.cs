using System.Collections;
using UnityEngine;

namespace Fantasie
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class BulletFly : MonoBehaviour, ICausingDamage
    {
        //todo take projectile stats from SO
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Rigidbody2D _bulletBody;
        [SerializeField] private GameObject _bulletParent;
        [SerializeField] private GameObject _firePoint;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _shootingForce;
        [SerializeField] private float _damage;

        public float GetDamage { get => _damage; }

        private void OnEnable()
        {
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