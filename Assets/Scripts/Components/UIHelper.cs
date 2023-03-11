using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Fantasie
{
    public class UIHelper : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthToString;
        [SerializeField] private TMP_Text _energyToString;
        [SerializeField] private TMP_Text _goldToString;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _energySlider;
        [SerializeField] private Health _healthComponent;
        [SerializeField] private UltimateEnergy _energyComponent;
        [SerializeField] private PickUpItem _pickup;

        private float _hpSliderValueModif = 100;
        private float _energySliderValueModif = 100;
        private float _hpValue = 0;
        private float _energyValue = 0;
        private int _goldValue = 0;
        private int _gameSceneIndex = 1;

        public int GameSceneIndex
        {
            get => _gameSceneIndex;
            set => _gameSceneIndex = value;
        }

        private void LateUpdate()
        {
            HealthUpdate();
            UltimateUpdate();
            GoldUpdate();
        }

        public void StartGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: GameSceneIndex);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void HealthUpdate()
        {
            _hpValue = _healthComponent.GetHealth;
            _healthToString.text = _hpValue.ToString();
            _healthSlider.value = _hpValue/ _hpSliderValueModif;
        }

        private void UltimateUpdate()
        {
            _energyValue = _energyComponent.GetEnergy;
            _energyToString.text = _energyValue.ToString();
            _energySlider.value = _energyValue / _energySliderValueModif;
        }
        private void GoldUpdate()
        {
            _goldValue = _pickup.GetGold;
            _goldToString.text = "Gold: " + _goldValue.ToString();
        }
    }
}