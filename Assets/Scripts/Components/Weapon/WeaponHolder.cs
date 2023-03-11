using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

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
                    _weaponList[i].TryGetComponent(out ItemSpriteAnimate spriteAnimate);
                    firstImage.sprite = renderer.sprite;
                    firstImage.gameObject.SetActive(true);
                    spriteAnimate.enabled = false;
                }
            }
        }

        public void ChangeWeaponData(int index)
        {
            if (_weaponList[index] == null) return;

            _weaponList[index].TryGetComponent(out WeaponData data);
            _currentData.TakeAnotherWeapon(data);
        }
    }
}