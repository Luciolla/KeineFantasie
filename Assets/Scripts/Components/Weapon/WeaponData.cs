using System;
using UnityEngine;

namespace Fantasie
{
    public class WeaponData : MonoBehaviour //I wanna do it through scriptableObjects, but postponed it until better times
    {
        [SerializeField] private String _name = "Default";
        [SerializeField] private SpriteRenderer _weaponRenderer;
        [SerializeField] private Sprite _weaponSprite;
        [SerializeField] private RuntimeAnimatorController _projectileAnimation;
        [SerializeField] private float _projectileDamage;
        [SerializeField] private float _projectileHeavyDamage;
        [SerializeField] private float _projectileLifeTime;
        [SerializeField] private float _weaponShootForce;

        public String GetName => _name;
        public Sprite GetWeaponSprite => _weaponSprite;
        public RuntimeAnimatorController GetProjectileAnimation => _projectileAnimation;
        public float GetProjectileDamage => _projectileDamage;
        public float GetProjectileHeavyDamage => _projectileHeavyDamage;
        public float GetProjectileLifeTime => _projectileLifeTime;
        public float GetWeaponShootForce => _weaponShootForce;

        public void TakeAnotherWeapon(WeaponData data)
        {
            _name = data.GetName;
            _weaponSprite = data.GetWeaponSprite;
            _projectileAnimation = data.GetProjectileAnimation;
            _projectileDamage = data.GetProjectileDamage;
            _projectileHeavyDamage = data.GetProjectileHeavyDamage;
            _projectileLifeTime = data.GetProjectileLifeTime;
            _weaponShootForce = data.GetWeaponShootForce;
            _weaponRenderer.sprite = _weaponSprite;
        }
    }
}