using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fantasie
{
    public class EnemyActivate : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private List<GameObject> _enemy = new();

        private void Start()
        {
            StartCoroutine(ActivateEnemy());
        }

        private IEnumerator ActivateEnemy()
        {
            if (_target == null) yield break;

            while (true)
            {
                for (var i = 0; i < _enemy.Count; i++)
                {
                    if (Vector2.Distance(_target.transform.position, _enemy[i].transform.position) < 30)
                    {
                        _enemy[i].TryGetComponent(out Health health);
                        if (health.GetHealth <= 0) _enemy.Remove(_enemy[i].gameObject);
                        else
                            _enemy[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        _enemy[i].gameObject.SetActive(false);
                    }
                }

                yield return new WaitForSecondsRealtime(1f);
            }
        }
    }
}