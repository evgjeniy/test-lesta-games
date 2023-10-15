using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerJumpEvent> _jumpEventFilter;
        private readonly EcsFilter<RigidbodyComponent, JumpComponent, PlayerTag> _playerJumpFilter;
        private readonly PlayerSettings _settings;
        
        public void Run()
        {
            foreach (var entityId in _playerJumpFilter)
            {
                ref var entity = ref _playerJumpFilter.GetEntity(entityId);
                ref var rigidbody = ref _playerJumpFilter.Get1(entityId).rigidbody;

                if (ContainsTrampolineRequest(entity, rigidbody)) continue;
                
                ref var jumpComponent = ref _playerJumpFilter.Get2(entityId);
                jumpComponent.isGrounded = Physics.CheckSphere(
                    jumpComponent.triggerCenter.position,
                    jumpComponent.triggerRadius,
                    jumpComponent.triggerMask.value);

                if (jumpComponent.isGrounded is false || _jumpEventFilter.IsEmpty()) continue;
                
                rigidbody.velocity = CalculateVelocity(rigidbody.velocity, _settings.jumpForce);
            }
        }

        private static bool ContainsTrampolineRequest(EcsEntity entity, Rigidbody rigidbody)
        {
            if (entity.Has<TrampolineJumpRequest>() is false) return false;
            
            var trampolineJumpForce = entity.Get<TrampolineJumpRequest>().jumpForce;
            rigidbody.velocity = CalculateVelocity(rigidbody.velocity, trampolineJumpForce);
            return true;
        }

        private static Vector3 CalculateVelocity(Vector3 old, float y) => new(old.x, y, old.z);
    }
}