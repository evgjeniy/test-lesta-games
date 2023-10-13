using Ecs.Components;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMovementRequest> _movementRequests = null;
        private readonly EcsFilter<RigidbodyRef, PlayerTag> _playerFilter = null;
        private readonly PlayerSettings _settings = null;
        
        public void Run()
        {
            if (_movementRequests.IsEmpty()) return;
            ref var movementRequest = ref _movementRequests.Get1(0);
            var speed = _settings.moveSpeed * (movementRequest.isRunning ? _settings.runMultiplier : 1.0f);
                
            foreach (var entityId in _playerFilter)
            {
                ref var transform = ref _playerFilter.Get1(entityId).rigidbody;
                transform.position += movementRequest.moveDirection * (speed * Time.deltaTime);

                var targetRotation = Quaternion.LookRotation(movementRequest.moveDirection);
                var tLerp = _settings.rotationSmoothSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tLerp);
            }
        }
    }
}