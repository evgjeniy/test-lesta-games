using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Tags;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerJumpEvent> _jumpEventFilter = null;
        private readonly EcsFilter<RigidbodyRef, JumpComponent, PlayerTag> _playerJumpFilter = null;
        private readonly PlayerSettings _settings;
        
        public void Run()
        {
            foreach (var entityId in _playerJumpFilter)
            {
                ref var rigidbody = ref _playerJumpFilter.Get1(entityId).rigidbody;
                ref var jumpComponent = ref _playerJumpFilter.Get2(entityId);

                jumpComponent.isGrounded = Physics.CheckSphere(
                    jumpComponent.triggerCenter.position,
                    jumpComponent.triggerRadius,
                    jumpComponent.triggerMask.value);

                if (!jumpComponent.isGrounded) continue;
                if (_jumpEventFilter.IsEmpty()) continue;
                
                var oldVelocity = rigidbody.velocity;
                rigidbody.velocity = new Vector3(oldVelocity.x, _settings.jumpForce, oldVelocity.z);
            }
        }
    }
}