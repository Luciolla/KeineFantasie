using UnityEngine;

namespace Fantasie
{
    public class WeaponDamage : MonoBehaviour, ICausingDamage
    {
        [SerializeField] private float _damage;

        public float GetDamage { get => _damage; }
    }
}