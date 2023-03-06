using UnityEngine;

namespace Fantasie
{
    public class SearchTarget : MonoBehaviour
    {
        [SerializeField] private EnemyAiming _enemyAiming;
        [SerializeField] private BotController _botController;
        [SerializeField] private CircleCollider2D _circleCollider;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other == null) return;
            var status = other.TryGetComponent(out CreatureType type);
            if (type.GetCreatureType != CreatureTypeEnum.Player) return;

            PointoutShootTarget(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other == null) return;
            var status = other.TryGetComponent(out CreatureType type);
            if (type.GetCreatureType != CreatureTypeEnum.Player) return;

            PointoutShootTarget(null);
        }

        private void PointoutShootTarget(GameObject value)
        {
            _enemyAiming.SetTarget = value;
            _botController.SetTarget = value;
        }
    }
}