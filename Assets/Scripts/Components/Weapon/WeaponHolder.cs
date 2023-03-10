using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fantasie
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _weaponList;
        [SerializeField] private List<Image> _UIWeaponImages;
        [SerializeField] private GameObject _weaponAnchor;
        [SerializeField] private WeaponData _currentData;

        public GameObject GetWeapon
        {
            set => _weaponList.Add(value);
        }

        private void Start()
        {
            ApplySprites();
        }

        public void AddToWeaponList(GameObject weapon)
        {
            _weaponList.Add(weapon);
            ApplySprites();
        }

        public void ApplySprites()
        {
            for (var i = 0; i < _weaponList.Count; i++)
            {
                if (_weaponList[i] != null)
                {
                    var firstImage = _UIWeaponImages[i];
                    _weaponList[i].TryGetComponent(out SpriteRenderer renderer);
                    firstImage.sprite = renderer.sprite;
                    firstImage.gameObject.SetActive(true);
                }
            }
        }

        public void ChangeWeaponData(int index)
        {
            if (index > _weaponList.Count-1) return;

            _weaponList[index].TryGetComponent(out WeaponData data);
            _currentData.TakeAnotherWeapon(data);
        }
    }
}