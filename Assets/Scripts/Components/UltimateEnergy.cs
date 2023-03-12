using System.Collections;
using UnityEngine;

namespace Fantasie
{
    public class UltimateEnergy : MonoBehaviour
    {
        [SerializeField] private float _maxEnergy;
        [SerializeField] private float _rechargeInMunutes;
        [SerializeField] private float _amountOfRecharge;

        private float _currentEnergy = 0;
        private int _rechargeModif = 60;

        public float GetEnergy
        {
            get =>_currentEnergy;
            set => _currentEnergy = value;
        }

        public bool IsEnergyEnough => _currentEnergy == _maxEnergy;

        private void Awake() => StartCoroutine(ChargeEnergyRutine());

        private IEnumerator ChargeEnergyRutine()
        {
            while (true)
            {
                _currentEnergy += _amountOfRecharge;
                yield return new WaitForSecondsRealtime(_rechargeModif/_rechargeInMunutes);
            }
        }
    }
}