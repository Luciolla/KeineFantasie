using UnityEngine;
using UnityEngine.InputSystem;

namespace Fantasie
{
    public class PlayerController : BaseInputController
    {
        [SerializeField] private GameObject _player;
        [Header("components links")]
        [SerializeField] private CheckLayer _checkLayer;
        [SerializeField] private ShootWeapon _weapon;
        [SerializeField] private Ultimate _ultimate;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;
        [SerializeField] private WeaponHolder _weaponHolder;
        [SerializeField] private StepsSound _stepsSound;

        private InputControls _controls;

        private float _xVelocity = 0f;
        private float _yVelocity = 0f;
        private float _doubleJumpModif = 1f;
        private int _jumpCount = 0;
        private bool _isOnGround = false;

        private void Awake()
        {
            _controls = new();
            _controls.Player.Jump.performed += _ => CallJump(true);
            _controls.Player.Jump.canceled += _ => CallJump(false);
            _controls.Player.Shoot.performed += _ => CallShoot(true);
            _controls.Player.Shoot.canceled += _ => CallShoot(false);
            _controls.Player.HeavyShoot.performed += _ => CallHeavyShoot(true);
            _controls.Player.HeavyShoot.canceled += _ => CallHeavyShoot(false);
            _controls.Player.CastUltimate.performed += _ => CallUltimate(true);
            _controls.Player.CastUltimate.canceled += _ => CallUltimate(false);
        }

        private void OnEnable()
        {
            OnJumpEvent += OnJump;
            OnShootEvent += OnShoot;
            OnHeavyShootEvent += OnHeavyShoot;
            OnUltimateEvent += OnUltimate;
            _controls.Player.Enable();
        }

        private void Update()
        {
            _isOnGround = IsGrounded();
        }

        protected override void FixedUpdate()
        {
            OnMovement();
            _isOnGround = IsGrounded();
        }

        private void LateUpdate()
        {
            ApplyAnimation();
            OnWeaponChange();
            StepSoundOn();
            UpdateSpriteDirection();
        }

        private bool IsGrounded()
        {
            return _checkLayer.IsTouchingLayer;
        }

        private void OnMovement()
        {
            GetDirection = _controls.Player.Motion.ReadValue<Vector2>();
            if (GetDirection == null) return;
            if (_isOnGround) _jumpCount = 0;

            _xVelocity = GetDirection.x * (_speed);
            _yVelocity = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = new Vector2(_xVelocity, _yVelocity);
        }

        private void OnJump(bool value)
        {
            if (_jumpCount >= 1) return;

            if (value)
            {
                _jumpCount++;
                var djm = _jumpCount > 0 ? _doubleJumpModif = 1.2f : _doubleJumpModif = 1f;

                _rigidbody2D.AddForce(Vector3.up * _jumpspeed * _doubleJumpModif, ForceMode2D.Impulse);
            }
        }

        private void OnShoot(bool value)
        {
            var shoot = value ? _weapon.SetCanShoot = true : _weapon.SetCanShoot = false;
        }

        private void OnHeavyShoot(bool value)
        {
            var shoot = value ? _weapon.SetCanHeavyShoot = true : _weapon.SetCanHeavyShoot = false;
        }

        private void OnUltimate(bool value)
        {
            var shoot = value ? _ultimate.SetCanUltimateShoot = true : _ultimate.SetCanUltimateShoot = false;
        }

        private void OnWeaponChange()
        {
            if (Keyboard.current[Key.Digit1].isPressed)
            {
                _weaponHolder.ChangeWeaponData(0);
            }

            if (Keyboard.current[Key.Digit2].isPressed)
            {
                _weaponHolder.ChangeWeaponData(1);
            }

            if (Keyboard.current[Key.Digit3].isPressed)
            {
                _weaponHolder.ChangeWeaponData(2);
            }
        }

        private void ApplyAnimation()
        {
            _animator.SetBool("isGrounded", _isOnGround);
            _animator.SetBool("isRun", _rigidbody2D.velocity.x != 0 && _isOnGround);
            _animator.SetBool("isJump", _rigidbody2D.velocity.y != 0);
        }

        private void UpdateSpriteDirection()
        {
            if (_rigidbody2D.velocity.x < 0 && _player.transform.localScale.x > 0)
            {
                _player.transform.localScale = new(_player.transform.localScale.x * -1, _player.transform.localScale.y, _player.transform.localScale.z);
            }
            else if (_rigidbody2D.velocity.x > 0 && _player.transform.localScale.x < 0)
            {
                _player.transform.localScale = new(_player.transform.localScale.x * -1, _player.transform.localScale.y, _player.transform.localScale.z);
            }
        }

        private void StepSoundOn()
        {
            if (_isOnGround)
            {
                var RigidMoveTrue = _rigidbody2D.velocity.x != 0
                    ? _stepsSound.IsMoving = true
                    : _stepsSound.IsMoving = false;
            }
            else _stepsSound.IsMoving = false;
        }

        private void OnDisable() => _controls.Player.Disable();

        private void OnDestroy()
        {
            OnJumpEvent -= OnJump;
            OnShootEvent -= OnShoot;
            OnHeavyShootEvent -= OnHeavyShoot;
            OnUltimateEvent -= OnUltimate;
            _controls.Dispose();
        }
    }
}