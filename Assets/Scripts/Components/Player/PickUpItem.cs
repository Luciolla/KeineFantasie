using System.Collections.Generic;
using UnityEngine;

namespace Fantasie
{
    public class PickUpItem : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider2D _collider2D;
        [SerializeField] private WeaponHolder _holder;
        [SerializeField] private AudioSource _source;
        [SerializeField] private List<AudioClip> _clips;

        private List<GameObject> _itemList; //todo
        private int _goldAmount;
        private bool _pickedUp = false;

        public int GetGold => _goldAmount;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.TryGetComponent(out Gold gold);
            if (gold == null) return;
            _goldAmount += gold.GetGold;
            _source.PlayOneShot(_clips[0]);
            gold.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.TryGetComponent(out ShootWeapon weapon);
            if (weapon == null && _pickedUp!=true) return;
            _pickedUp = true;
            weapon.gameObject.SetActive(false);
            _holder.GetWeapon = weapon.gameObject;
            _holder.ApplySprites();
            _source.PlayOneShot(_clips[1]);
            _pickedUp = false;
        }
    }
}