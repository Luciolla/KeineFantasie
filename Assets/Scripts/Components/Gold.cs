using UnityEngine;
using Random = System.Random;

namespace Fantasie
{
    public class Gold : MonoBehaviour
    {
        [Header("Min&Max amount of gold to drop")]
        [SerializeField] private int[] _goldRandomRange = new int[2];
        private Random rand = new();
        private int _goldAmount;

        public int GetGold => _goldAmount;

        private void OnEnable() => _goldAmount = rand.Next(_goldRandomRange[0], _goldRandomRange[1] + 1);
    }
}