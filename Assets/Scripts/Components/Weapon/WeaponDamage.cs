using UnityEngine;

namespace Fantasie
{
    public class WeaponDamage : MonoBehaviour, ICausingDamage
    {
        [SerializeField] private WeaponData _damageData;
        [SerializeField] private bool _isHeavy;

        private float _damage;
        private float _damageHeavy;

        private void OnEnable()
        {
            _damage = _damageData.GetProjectileDamage;
            _damageHeavy = _damageData.GetProjectileHeavyDamage;
        }

        public float GetDamage => _damage;
        public float GetHeavyDamage => _damageHeavy;
        public bool IsHeavy => _isHeavy;
    }
}