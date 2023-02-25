using System.Collections;
using UnityEngine;

namespace Fantasie
{
    public class BulletFly : MonoBehaviour
    {
        //todo take projectile stats from SO
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Rigidbody2D _bulletBody;
        [SerializeField] private GameObject _bulletParent;
        [SerializeField] private GameObject _firePoint;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _shootingForce;
        //[SerializeField] private float _damage;

        private void OnEnable()
        {
            ResetTransform();
            StartCoroutine(FlyBulletRutine());
        }

        private void ResetTransform()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.SetParent(null);
        }

        private IEnumerator FlyBulletRutine()
        {
            while (true)
            {
                _bulletBody.AddForce(transform.up * _shootingForce, ForceMode2D.Impulse);

                yield return new WaitForSecondsRealtime(_lifeTime);
                _bullet.gameObject.SetActive(false);
                transform.SetParent(_firePoint.transform);
                transform.localPosition = Vector3.zero;
                transform.localRotation = _firePoint.transform.localRotation;
                yield break;
            }
        }
    }
}