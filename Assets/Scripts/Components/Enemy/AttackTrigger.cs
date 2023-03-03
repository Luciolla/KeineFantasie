using UnityEngine;

namespace Fantasie
{
    public class AttackTrigger : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider2D;
        [SerializeField] private EnemyAiming _enemyAiming;

        public bool _canAttack = false;
        private bool _isAttackSuccesful = false;

        public bool GetCanAttack => _canAttack;
        public bool GetIsAttackSuccessful => _isAttackSuccesful;

        public void SuccessfulAttack() => _isAttackSuccesful = true;
        public void FailedAttack() => _isAttackSuccesful = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var status = other.TryGetComponent(out CreatureType type);
            if (type.GetCreatureType != CreatureTypeEnum.Player) return;
            PointoutShootTarget(other.gameObject);
            _canAttack = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var status = other.TryGetComponent(out CreatureType type);
            if (type.GetCreatureType != CreatureTypeEnum.Player) return;
            PointoutShootTarget(null);
            _canAttack = false;
        }

        private void PointoutShootTarget(GameObject value)
        {
            _enemyAiming.SetTarget = value;
        }
    }
}