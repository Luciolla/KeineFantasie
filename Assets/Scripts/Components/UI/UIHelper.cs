using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Fantasie
{
    public class UIHelper : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthToString;
        [SerializeField] private TMP_Text _energyToString;
        [SerializeField] private TMP_Text _goldToString;
        [SerializeField] private GameObject _panelMenu;
        [SerializeField] private GameObject _panelDeath;
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
        private bool _isPauseMenuOpened = false;

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
            CheckForESC();
            CheckForDeath();
        }

        public void StartGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: GameSceneIndex);
            Time.timeScale = 1f;
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void CheckForESC()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenMenu();
            }
        }

        private void CheckForDeath()
        {
            if (_hpValue <= 0)
                OpenDeathScreen();
        }

        private void OpenDeathScreen()
        {
            if (_panelDeath == null) return;
            OpenCloseMenu(_panelDeath, false);
        }

        private void OpenMenu()
        {
            OpenCloseMenu(_panelMenu, _isPauseMenuOpened);
            _isPauseMenuOpened = !_isPauseMenuOpened;
        }

        private void OpenCloseMenu(GameObject obj, bool activity)
        {
            obj.gameObject.SetActive(!activity);

            var timeStop = !activity ? Time.timeScale = 0.00001f : Time.timeScale = 1f;
        }

        private void HealthUpdate()
        {
            if (_healthComponent == null) return;
            _hpValue = _healthComponent.GetHealth;
            _healthToString.text = _hpValue.ToString();
            _healthSlider.value = _hpValue / _hpSliderValueModif;
        }

        private void UltimateUpdate()
        {
            if (_energyComponent == null) return;
            _energyValue = _energyComponent.GetEnergy;
            _energyToString.text = _energyValue.ToString();
            _energySlider.value = _energyValue / _energySliderValueModif;
        }
        private void GoldUpdate()
        {
            if (_pickup == null) return;
            _goldValue = _pickup.GetGold;
            _goldToString.text = "Gold: " + _goldValue.ToString();
        }
    }
}