using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        private int _gameSceneIndex = 1;

        public int GameSceneIndex
        {
            get => _gameSceneIndex;
            set => _gameSceneIndex = value;
        }

        private void LateUpdate()
        {
            HealthUpdate();
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
    }
}