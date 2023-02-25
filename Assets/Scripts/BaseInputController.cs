using System;
using UnityEngine;

namespace Fantasie
{
    public abstract class BaseInputController : MonoBehaviour
    {
        public event Action<bool> OnJumpEvent;
        public event Action<bool> OnShootEvent;
        
        protected float _speed = 5f;
        protected float _jumpspeed = 5f;
        protected float _damageJumpspeed = 3f;
        protected float _interactionRadius = 1f;

        public Vector2 Direction { get; protected set; }

        protected void CallJump(bool value) => OnJumpEvent?.Invoke(value);
        protected void CallShoot(bool value) => OnShootEvent?.Invoke(value);

        protected abstract void FixedUpdate();
    }
}