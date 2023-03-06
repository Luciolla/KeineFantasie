using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Fantasie
{
    public class BotController : BaseInputController //todo separate to some new components
    {
        [SerializeField] private EnemyTypeEnum _enemyType;
        [SerializeField] private EnemyBehaviourEnum _enemyBehaviour;
        [SerializeField] private GameObject _enemy;
        [SerializeField] private List<GameObject> _weaponList; //todo fix crutch
        [SerializeField] private float _patrolRadius;
        [Header("components links")]
        [SerializeField] private CheckLayer _checkLayer;
        [SerializeField] private ShootWeapon _weapon;
        [SerializeField] private AttackTrigger _attackTrigger;
        [SerializeField] private EnemyAiming _enemyAiming;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;

        private Random _rand = new();
        private Vector2 _startPosition;
        private GameObject _target;
        private float _xVelocity = 0f;
        private float _yVelocity = 0f;
        private int _jumpCount = 0;
        private int _patrolState = 0;
        private int _lastPatrolState = 0;
        private int _reflectionCooldown = 2;
        private int _offset = 2;
        private bool _isOnGround = false;
        private bool _isNotSeePlayer = true;
        private bool _isAttack = false;
        private bool _isNoAttackCooldown = true;
        private bool _isEnemyMelee = true;

        public GameObject SetTarget
        {
            get => _target;
            set => _target = value;
        }

        private void Awake()
        {
            _startPosition = transform.position;
            CheckEnemyMelee();
            StartCoroutine(StopEnemyForSomeReflectionRutine());
        }

        protected override void FixedUpdate()
        {
            OnMovement();
            ApplyAnimation();
            ApplyPatrol(_patrolState);
            PrimitiveAISolutions();
            CheckForCauseDamage();
            _isOnGround = IsGrounded();
        }

        private bool IsGrounded()
        {
            return _checkLayer.IsTouchingLayer;
        }

        private void PrimitiveAISolutions()
        {
            if (_target == null)
            {
                CheckPatrolRadius();
            }
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

            _xVelocity = Direction.x * (_speed * _speedMogdif);
            _yVelocity = _rigidbody2D.velocity.y;

            if (_target == null) _rigidbody2D.velocity = new Vector2(_xVelocity, _yVelocity);
            else
                transform.position
                    = Vector2.MoveTowards(transform.position, _target.transform.position,
                        (_speed * _speedMogdif * Time.deltaTime));            

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
            var shoot = value ? _weapon.SetCanShoot = true : _weapon.SetCanShoot = false;
        }

        private void ApplyAnimation()
        {
            _animator.SetBool("is-Grounded", _isOnGround);
            _animator.SetBool("is-Attack", _attackTrigger.GetCanAttack);

            if (_enemyType == EnemyTypeEnum.Melee || _enemyType == EnemyTypeEnum.Range)
            {
                _animator.SetBool("is-Walk", _rigidbody2D.velocity.x != 0 && _isOnGround);
                _animator.SetBool("is-Jump", _rigidbody2D.velocity.y != 0);
            }
        }

        private void CheckForCauseDamage()
        {
                if (_attackTrigger.GetCanAttack && _attackTrigger.GetIsAttackSuccessful && _isNoAttackCooldown && _isEnemyMelee)
                {
                    _isNoAttackCooldown = false;
                    StartCoroutine(CauseMeleeDamageRutine());
                }
            
                if (_attackTrigger.GetCanAttack && _isNoAttackCooldown && !_isEnemyMelee)
                {
                    _isNoAttackCooldown = false;
                    StartCoroutine(CauseRangeDamageRutine());
                }
        }

        private IEnumerator CauseMeleeDamageRutine()
        {
            ActiveWeapon(true);
            yield return new WaitForSecondsRealtime(.1f);
            ActiveWeapon(false);
            yield return new WaitForSecondsRealtime(.5f);
            _isNoAttackCooldown = true;
            yield break;
        }

        private IEnumerator CauseRangeDamageRutine()
        {
            OnShoot(true);
            yield return new WaitForSecondsRealtime(.1f);
            OnShoot(false);
            yield return new WaitForSecondsRealtime(.5f);
            _isNoAttackCooldown = true;
            yield break;
        }

        private void ActiveWeapon(bool value)
        {
            foreach (var weapon in _weaponList)
                weapon.gameObject.SetActive(value);
        }

        private void CheckEnemyMelee()
        {
            var type = true;
            switch (_enemyType)
            {
                case EnemyTypeEnum.Melee:
                case EnemyTypeEnum.FlyMelee:
                    type = true;
                    break;
                case EnemyTypeEnum.Range:
                case EnemyTypeEnum.FlyRange:
                    type = false;
                    break;
            }
            _isEnemyMelee = type;
        }
    }
}