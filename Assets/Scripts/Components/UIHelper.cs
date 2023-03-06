using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantasie
{
    public class UIHelper : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthToString;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Health _healthComponent;

        private float _hpSliderValueModif = 100;
        private float _hpValue = 0;

        private void LateUpdate()
        {
            HealthUpdate();
        }

        private void HealthUpdate()
        {
            _hpValue = _healthComponent.GetHealth;
            _healthToString.text = _hpValue.ToString();
            _healthSlider.value = _hpValue/ _hpSliderValueModif;
        }
    }
}