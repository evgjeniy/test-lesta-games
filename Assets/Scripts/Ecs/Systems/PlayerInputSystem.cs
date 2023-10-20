using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
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
        private readonly EcsFilter<ReverseMovementEvent> _reverseEventsFilter;

        private readonly EcsWorld _world;
        private PlayerInput _input;

        public void Init()
        {
            _input = new PlayerInput();
            _input.Player.Jump.performed += SendJumpEvent;
        }

        public void Destroy() => _input.Player.Jump.performed -= SendJumpEvent;

        private void SendJumpEvent(InputAction.CallbackContext _) => _world.SendMessage<PlayerJumpEvent>();

        public void Run()
        {
            if (_enableFilter.IsEmpty() is false)
            {
                _input.Enable();
                foreach (var entityId in _enableFilter)
                    _enableFilter.GetEntity(entityId).Del<PlayerEnableInputEvent>();
            }

            if (_disableFilter.IsEmpty() is false)
            {
                _input.Disable();
                foreach (var entityId in _enableFilter)
                    _disableFilter.GetEntity(entityId).Del<PlayerDisableInputEvent>();
            }
            
            ReadMoveInput();
            ReadMouseLookInput();
        }

        private void ReadMoveInput()
        {
            var inputDirection = _input.Player.Move.ReadValue<Vector2>();
            if (!(inputDirection.magnitude >= Constants.normalizedMoveSpeed.x)) return;

            if (_reverseEventsFilter.IsEmpty() is false)
            {
                inputDirection *= -1.0f;
                foreach (var entityId in _reverseEventsFilter)
                    _reverseEventsFilter.GetEntity(entityId).Del<ReverseMovementEvent>();
            }
            
            _world.GetEntity<PlayerTag>().SendMessage(new PlayerMovementRequest
            {
                direction = inputDirection,
                isRunning = _input.Player.Sprint.ReadValue<float>() != 0.0f
            });
        }

        private void ReadMouseLookInput()
        {
            var mouseLookDirection = _input.Player.Look.ReadValue<Vector2>();
            if (mouseLookDirection.magnitude == 0.0f) return;
            
            _world.SendMessage(new PlayerMouseMoveRequest { delta = mouseLookDirection });
        }
    }
}