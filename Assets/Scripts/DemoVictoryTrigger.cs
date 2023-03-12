using UnityEngine;

namespace Fantasie
{
    public class DemoVictoryTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject _victoryPanel;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _victoryPanel.gameObject.SetActive(true);
            Time.timeScale = .000001f;
        }
    }
}