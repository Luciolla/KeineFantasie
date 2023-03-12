using System;
using UnityEngine;

namespace Fantasie
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _itemToDrop;

        private float _currentHealth;

        public float GetHealth => _currentHealth;

        public float GetDamage
        {
            set => _currentHealth -= value;
        }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision == null) return;
            collision.gameObject.TryGetComponent(out WeaponDamage weapon);
            if (weapon == null) return;

            //CheckIsCanDamage(collision.gameObject);

            if (weapon.IsHeavy) _currentHealth -= weapon.GetHeavyDamage;
            else _currentHealth -= weapon.GetDamage;

            if (_currentHealth <= 0)
            {
                _itemToDrop.SetActive(true);
                _itemToDrop.transform.SetParent(null);
                gameObject.SetActive(false);
            }
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