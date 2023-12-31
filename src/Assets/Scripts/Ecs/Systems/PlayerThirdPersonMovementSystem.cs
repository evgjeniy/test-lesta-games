﻿using Ecs.Components;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerThirdPersonMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMovementRequest> _movementRequests;
        private readonly EcsFilter<RigidbodyComponent, ThirdPersonCamera, PlayerTag> _thirdPersonPlayerFilter;
        private readonly PlayerSettings _settings;
        
        public void Run()
        {
            foreach (var entityId in _thirdPersonPlayerFilter)
            {
                ref var entity = ref _thirdPersonPlayerFilter.GetEntity(entityId);
                ref var rigidbodyComponent = ref _thirdPersonPlayerFilter.Get1(entityId);
                ref var thirdPersonMovementComponent = ref _thirdPersonPlayerFilter.Get2(entityId);
                
                var rigidbody = rigidbodyComponent.rigidbody;
                var cameraTransform = thirdPersonMovementComponent.camera.transform;

                var moveDirection = GetMoveDirection(cameraTransform, rigidbody);

                if (entity.Has<WindEffectRequest>())
                {
                    ref var windEffect = ref entity.Get<WindEffectRequest>();
                    moveDirection += windEffect.windSpeedValue * windEffect.windDirection.normalized;
                }
                
                rigidbody.position += moveDirection * Time.deltaTime;
            }
        }

        private Vector3 GetMoveDirection(Transform cameraTransform, Rigidbody rigidbody)
        {
            if (_movementRequests.IsEmpty()) return Vector3.zero;
            ref var movementRequest = ref _movementRequests.Get1(0);
            
            var moveDirection = GetMoveDirectionFromCamera(movementRequest.direction, cameraTransform);
            moveDirection *= _settings.moveSpeed * (movementRequest.isRunning ? _settings.runMultiplier : 1.0f);
            
            RotateToMoveDirection(rigidbody, moveDirection);

            return moveDirection;
        }

        private static Vector3 GetMoveDirectionFromCamera(Vector2 inputDirection, Transform cameraTransform)
        {
            var resultMoveDirection = new Vector3();

            resultMoveDirection += cameraTransform.right * inputDirection.x;
            resultMoveDirection += cameraTransform.forward * inputDirection.y;
            resultMoveDirection.y = 0.0f;

            return resultMoveDirection.normalized;
        }

        private void RotateToMoveDirection(Rigidbody rigidbody, Vector3 moveDirection)
        {
            var targetRotation = Quaternion.LookRotation(moveDirection);
            var lerpValue = _settings.rotationSmoothSpeed * Time.deltaTime;
            rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, lerpValue);
        }
    }
}