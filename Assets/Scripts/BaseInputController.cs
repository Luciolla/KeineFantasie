using System;
using UnityEngine;

namespace Fantasie
{
    public abstract class BaseInputController : MonoBehaviour
    {
        public event Action<bool> OnJumpEvent;
        public event Action<bool> OnShootEvent;
        public event Action<bool> OnHeavyShootEvent;
        public event Action<bool> OnUltimateEvent;

        [SerializeField] protected float _speedMogdif = 1;
        protected float _speed = 5f;
        protected float _jumpspeed = 5f;
        protected float _damageJumpspeed = 3f;
        protected float _interactionRadius = 1f;

        public Vector2 Direction { get; protected set; }

        protected void CallJump(bool value) => OnJumpEvent?.Invoke(value);
        protected void CallShoot(bool value) => OnShootEvent?.Invoke(value);
        protected void CallHeavyShoot(bool value) => OnHeavyShootEvent?.Invoke(value);
        protected void CallUltimate(bool value) => OnUltimateEvent?.Invoke(value);

        protected abstract void FixedUpdate();
    }
}