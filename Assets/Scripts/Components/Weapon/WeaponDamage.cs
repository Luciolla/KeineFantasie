using UnityEngine;

namespace Fantasie
{
    public class WeaponDamage : MonoBehaviour, ICausingDamage
    {
        [SerializeField] private WeaponData _damageData;

        private float _damage;

        private void OnEnable() => _damage = _damageData.GetProjectileDamage;
        public float GetDamage => _damage;
    }
}