using UnityEngine;

namespace Fantasie
{
    public class PlayerController : BaseInputController
    {
        [SerializeField] private GameObject _player;
        [Header("components links")]
        [SerializeField] private CheckLayer _checkLayer;
        [SerializeField] private ShootWeapon _weapon;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;

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
        }

        private void OnEnable()
        {
            OnJumpEvent += OnJump;
            OnShootEvent += OnShoot;
            _controls.Player.Enable();
        }

        private void Update()
        {
            _isOnGround = IsGrounded();
        }

        protected override void FixedUpdate()
        {
            OnMovement();
            ApplyAnimation();
            UpdateSpriteDirection();
            _isOnGround = IsGrounded();
        }

        private bool IsGrounded()
        {
            return _checkLayer.IsTouchingLayer;
        }

        private void OnMovement()
        {
            Direction = _controls.Player.Motion.ReadValue<Vector2>();

            if (Direction == null) return;
            if (_isOnGround) _jumpCount = 0;

            _xVelocity = Direction.x * _speed;
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
            if (value)
                _weapon.SetCanShoot = value;
            else 
                _weapon.SetCanShoot = false; //dunno is it relevant
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

        private void OnDisable() => _controls.Player.Disable();

        private void OnDestroy()
        {
            OnJumpEvent -= OnJump;
            OnShootEvent -= OnShoot;
            _controls.Dispose();
        }
    }
}