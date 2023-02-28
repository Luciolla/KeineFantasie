using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fantasie
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Animator _animator;

        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision == null) return;
            collision.gameObject.TryGetComponent(out BulletFly bullet);
            if (bullet == null) return;

            //CheckIsCanDamage(collision.gameObject);

            Debug.Log(_currentHealth);
            _currentHealth -= bullet.GetDamage;
            if (_currentHealth <= 0)
                gameObject.SetActive(false);
            else
                Debug.Log(_currentHealth);
        }
#if UNITY_EDITOR
        #region testInterface
        //почему нельзя просто проверить, связан ли объект с интерфейсом...
        //чет как то изыскания по этому вопросу ничего не дали...
        //то, что дали - ниже... но оно как то не работает, чтоли...

        //private bool CheckIsCanDamage(GameObject obj) 
        //{
        //    if (obj is ICausingDamage)
        //    {
        //        Debug.Log(_currentHealth);
        //        return true;
        //    } 
        //    else return false;
        //}
        #endregion
#endif

    }
}