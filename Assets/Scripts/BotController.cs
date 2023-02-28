using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = System.Random;

namespace Fantasie
{
    public class BotController : BaseInputController
    {
        [SerializeField] private EnemyTypeEnum _enemyTypeType;
        [SerializeField] private GameObject _enemy;
        [Header("AI Weights modif")]
        [SerializeField, Range(-1, 1)] private float _stopModif;
        [SerializeField, Range(-1, 1)] private float _jumpModif;
        [SerializeField, Range(-1, 1)] private float _attackModif;
        [SerializeField, Range(-1, 1)] private float _changeWalkSideModif;
        [Header("components links")]
        [SerializeField] private CheckLayer _checkLayer;
        [SerializeField] private ShootWeapon _weapon;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;

        private Random _rand = new();
        private float _xVelocity = 0f;
        private float _yVelocity = 0f;
        private int _jumpCount = 0;
        private bool _isOnGround = false;
        private bool _isNotSeePlayer = true;
        private bool _isCooldown = false;

        #region AIWeights
        private float _currentWalkSide;
        private float _currentJumpWieght;
        private float _currentattackieght;
        private float _currentStopWieght;
        #endregion

        protected override void FixedUpdate()
        {
            OnMovement();
            ApplyAnimation();
            UpdateSpriteDirection();
            PrimitiveAISolutions();
            _isOnGround = IsGrounded();
        }
        private bool IsGrounded()
        {
            return _checkLayer.IsTouchingLayer;
        }

        private void PrimitiveAISolutions() //todo separate to new component 
        {
            if (_isNotSeePlayer)
            {
                if (!_isCooldown)
                {
                    _currentWalkSide = _rand.Next(-1, 2);

                    Direction = _currentWalkSide == 0 ? Vector2.zero : Vector2.right * _currentWalkSide;
                    StartCoroutine(ÑooldownRutine(_rand.Next(1, 3)));
                }
            }

        }

        private IEnumerator ÑooldownRutine(int cooldown)
        {
            _isCooldown = true;
            yield return new WaitForSecondsRealtime(cooldown);
            _isCooldown = false;
            yield break;
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