using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Logic.Player
{
    public class HeroInput : MonoBehaviour
    {
        public event Action<Vector2> OnMove;
        
        private Input _input;

        private void OnEnable()
        {
            if (_input == null)
            {
                _input = new Input();
                Subscribe();
            }

            _input.Enable();
        }

        private void OnDisable()
        {
            UnSubscribe();
            _input.Disable();
        }

        private void Subscribe()
        {
            _input.PlayerInput.HorizontalMove.performed += MovePerformed();
            _input.PlayerInput.HorizontalMove.canceled += MoveCancelled();
        }

        private void UnSubscribe()
        {
            _input.PlayerInput.HorizontalMove.performed -= MovePerformed();
            _input.PlayerInput.HorizontalMove.canceled -= MoveCancelled();
        }

        private Action<InputAction.CallbackContext> MovePerformed() => 
            ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());

        private Action<InputAction.CallbackContext> MoveCancelled() => 
            ctx => OnMove?.Invoke(Vector2.zero);
    }
}