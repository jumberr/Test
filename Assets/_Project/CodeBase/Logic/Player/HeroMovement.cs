using UnityEngine;

namespace _Project.CodeBase.Logic.Player
{
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private HeroInput _heroInput;
        [SerializeField] private HeroAnimation _heroAnimation;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _speed;

        private float _xDirection;

        private void OnEnable() => 
            _heroInput.OnMove += ChangeMoveDirection;

        private void FixedUpdate() => 
            Move();

        private void OnDisable() =>
            _heroInput.OnMove -= ChangeMoveDirection;

        private void ChangeMoveDirection(Vector2 dir)
        {
            _xDirection = dir.x;
            _heroAnimation.Run(_xDirection);
        }

        private void Move()
        {
            var direction = Vector3.forward + new Vector3(_xDirection, 0, 0);
            _controller.Move(direction * _speed * Time.deltaTime);
        }
    }
}