using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Utilities;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ecs.Systems
{
    public class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<PlayerEnableInputEvent> _enableFilter;
        private readonly EcsFilter<PlayerDisableInputEvent> _disableFilter;
        
        private readonly EcsWorld _world;
        private PlayerInput _input;

        public void Init()
        {
            _input = new PlayerInput();
            _input.Player.Jump.performed += JumpOnPerformed;
            _input.Enable();
        }

        public void Destroy() => _input.Player.Jump.performed -= JumpOnPerformed;

        private void JumpOnPerformed(InputAction.CallbackContext obj) => _world.SendMessage<PlayerJumpEvent>();

        public void Run()
        {
            if (_enableFilter.IsEmpty() is false) _input.Enable();
            if (_disableFilter.IsEmpty() is false) _input.Disable();
            
            var direction = _input.Player.Move.ReadValue<Vector2>();
            if (direction.magnitude >= Constants.normalizedMoveSpeed.x)
                _world.SendMessage(new PlayerMovementRequest
                {
                    moveDirection = new Vector3(direction.x, 0.0f, direction.y),
                    isRunning = _input.Player.Sprint.ReadValue<float>() != 0.0f
                });
            
            // TODO - read input for mouse look
        }
    }
}