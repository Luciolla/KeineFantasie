using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = System.Random;

namespace Fantasie
{
    public class BotController : BaseInputController //todo separate to some new components
    {
        [SerializeField] private EnemyTypeEnum _enemyTypeType;
        [SerializeField] private EnemyBehaviourEnum _enemyBehaviour;
        [SerializeField] private GameObject _enemy;
        [SerializeField] private float _patrolRadius;
        [Header("components links")]
        [SerializeField] private CheckLayer _checkLayer;
        [SerializeField] private ShootWeapon _weapon;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;

        private Random _rand = new();
        private Vector2 _startPosition;
        private float _xVelocity = 0f;
        private float _yVelocity = 0f;
        private int _jumpCount = 0;
        private int _patrolState = 0;
        private int _lastPatrolState = 0;
        private int _reflectionCooldown = 2;
        private bool _isOnGround = false;
        private bool _isNotSeePlayer = true;
        private bool _isAttack = false;

        private void Awake()
        {
            _startPosition = transform.position;
            StartCoroutine(StopEnemyForSomeReflectionRutine());
        }

        protected override void FixedUpdate()
        {
            OnMovement();
            ApplyAnimation();
            UpdateSpriteDirection();
            PrimitiveAISolutions();
            CheckPatrolRadius();
            _isOnGround = IsGrounded();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var status = other.TryGetComponent(out CreatureType type);

            if (type.GetCreatureType != CreatureTypeEnum.Player) return;

            _isNotSeePlayer = false;
            Debug.Log("Я тебя, сука, вижу!");
            _patrolState = 1;
            _lastPatrolState = 1;

        }

        private bool IsGrounded()
        {
            return _checkLayer.IsTouchingLayer;
        }

        private void PrimitiveAISolutions()
        {
            //if (!_isNotSeePlayer) return;
            Debug.Log(_patrolState);
            ApplyPatrol(_patrolState);
        }

        private void ApplyPatrol(int state)
        {
            switch (state)
            {
                case 0:
                    Direction = Vector2.left;
                    break;
                case 1:
                    Direction = Vector2.zero;
                    break;
                case 2:
                    Direction = Vector2.right;
                    break;
            }
        }

        private IEnumerator StopEnemyForSomeReflectionRutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(_reflectionCooldown);
                _lastPatrolState = _patrolState;
                _patrolState = 1;
                yield return new WaitForSecondsRealtime(_reflectionCooldown);
                _patrolState = _lastPatrolState;
                _reflectionCooldown = _rand.Next(1, 5);
            }
        }

        private void CheckPatrolRadius()
        {
            if (_startPosition.x - transform.position.x > _patrolRadius)
            {
                _patrolState = 2;
            }
            if (_startPosition.x - transform.position.x < -_patrolRadius)
            {
                _patrolState = 0;
            }
        }

        private void OnMovement()
        {
            if (Direction == null) return;
            if (_isOnGround) _jumpCount = 0;

            _xVelocity = Direction.x * _speed;
            _yVelocity = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = new Vector2(_xVelocity, _yVelocity);
        }

        private void OnJump(bool value)
        {
            if (_jumpCount >= 0) return;

            if (value)
            {
                _jumpCount++;

                _rigidbody2D.AddForce(Vector3.up * _jumpspeed, ForceMode2D.Impulse);
            }
        }

        private void OnShoot(bool value)
        {
            if (value)
                _weapon.SetCanShoot = value;
            else
                _weapon.SetCanShoot = false;
        }

        private void ApplyAnimation()
        {
            _animator.SetBool("is-Grounded", _isOnGround);
            _animator.SetBool("is-Attack", _isAttack);
            _animator.SetBool("is-Walk", _rigidbody2D.velocity.x != 0 && _isOnGround);
            _animator.SetBool("is-Jump", _rigidbody2D.velocity.y != 0);
        }

        private void UpdateSpriteDirection()
        {
            if (_rigidbody2D.velocity.x > 0 && _enemy.transform.localScale.x > 0)
            {
                _enemy.transform.localScale = new(_enemy.transform.localScale.x * -1, _enemy.transform.localScale.y, _enemy.transform.localScale.z);
            }
            else if (_rigidbody2D.velocity.x < 0 && _enemy.transform.localScale.x < 0)
            {
                _enemy.transform.localScale = new(_enemy.transform.localScale.x * -1, _enemy.transform.localScale.y, _enemy.transform.localScale.z);
            }
        }
    }
}