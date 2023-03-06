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

        private void Start()
        {
            ApplySprotes();
        }

        private void ApplySprotes()
        {
            if (_weaponList[0] != null)
            {
                var firstImage = _UIWeaponImages[0];
                firstImage.gameObject.SetActive(true);
                _weaponList[0].TryGetComponent(out SpriteRenderer renderer);
                firstImage.sprite = renderer.sprite;
            }
        }
    }
}