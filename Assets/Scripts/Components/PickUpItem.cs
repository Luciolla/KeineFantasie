using System.Collections.Generic;
using UnityEngine;
using System;

namespace Fantasie
{
    public class PickUpItem : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider2D _collider2D;
        [SerializeField] private WeaponHolder _holder;

        private List<GameObject> _itemList; //todo
        private int _goldAmount;
        private bool _pickedUp = false;

        public int GetGold => _goldAmount;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.TryGetComponent(out Gold gold);
            if (gold == null) return;
            _goldAmount += gold.GetGold;
            gold.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("что это? " + collision);
            collision.gameObject.TryGetComponent(out ShootWeapon weapon);
            if (weapon == null && _pickedUp!=true) return;
            _pickedUp = true;
            weapon.gameObject.SetActive(false);
            _holder.GetWeapon = weapon.gameObject;
            _holder.ApplySprites();
            _pickedUp = false;
        }
    }
}