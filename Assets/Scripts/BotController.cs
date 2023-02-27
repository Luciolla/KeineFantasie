using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fantasie
{
    public class BotController : BaseInputController
    {
        [SerializeField] private EnemyTypeEnum _enemyTypeType;
        [SerializeField] private GameObject _enemy;
        [Header("components links")]
        [SerializeField] private CheckLayer _checkLayer;
        [SerializeField] private ShootWeapon _weapon;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;

        protected override void FixedUpdate()
        {

        }
    }
}