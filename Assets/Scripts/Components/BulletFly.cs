using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

        private int _side = 1;

        private void OnEnable()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.SetParent(null);
            StartCoroutine(FlyBulletRutine());
        }

        private IEnumerator FlyBulletRutine()
        {
            var direction = (Input.mousePosition - _firePoint.transform.position).normalized;
            var clearDirection = Input.mousePosition;

            _side = clearDirection.x < 500 ? -1 : 1;
            
            while (true)
            {
                _bulletBody.AddForce(direction * (_shootingForce * _side), ForceMode2D.Impulse);
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