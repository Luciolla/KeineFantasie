using UnityEngine;

namespace Fantasie
{
    public class SearchTarget : MonoBehaviour
    {
        [SerializeField] private EnemyAiming _enemyAiming;
        [SerializeField] private BotController _botController;
        [SerializeField] private CircleCollider2D _circleCollider;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other == null) return;
            if (!other.CompareTag("Player")) return;
                other.TryGetComponent(out CreatureType type);
                if (type == null) return;
                if (type.GetCreatureType == CreatureTypeEnum.Player)
                    PointShootTarget(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other == null) return;
            if (!other.CompareTag("Player")) return;
            other.TryGetComponent(out CreatureType type);
            if (type == null) return;
            if (type.GetCreatureType != CreatureTypeEnum.Player) return;

            PointShootTarget(null);
        }

        private void PointShootTarget(GameObject value)
        {
            _enemyAiming.SetTarget = value;
            _botController.SetTarget = value;
        }
    }
}