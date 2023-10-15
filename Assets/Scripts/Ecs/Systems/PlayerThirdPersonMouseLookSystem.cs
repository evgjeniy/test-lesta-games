using Ecs.Components;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerThirdPersonMouseLookSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMouseMoveRequest> _mouseMoveFilter;
        private readonly EcsFilter<ThirdPersonCamera, PlayerTag> _thirdPersonPlayerFilter;
        private readonly PlayerSettings _settings;
        
        public void Run()
        {
            foreach (var entityId in _thirdPersonPlayerFilter)
            {
                ref var thirdPersonMovementComponent = ref _thirdPersonPlayerFilter.Get1(entityId);

                var cameraTransform = thirdPersonMovementComponent.camera.transform;
                var cameraPivotPosition = thirdPersonMovementComponent.cameraPivot.position;

                if (_mouseMoveFilter.IsEmpty() is false)
                {
                    var mouseDelta = _mouseMoveFilter.Get1(0).delta;
                    var newCameraRotation = thirdPersonMovementComponent.currentRotation;

                    newCameraRotation.x += mouseDelta.x * _settings.cameraSensitivity.x * Time.deltaTime;
                    newCameraRotation.y += mouseDelta.y * _settings.cameraSensitivity.y * Time.deltaTime;
                    newCameraRotation.y = CheckVerticalBounds(newCameraRotation.y);

                    thirdPersonMovementComponent.currentRotation = newCameraRotation;
                }

                var newCameraPosition = cameraPivotPosition;
                newCameraPosition += SphericalCoordinates(thirdPersonMovementComponent.currentRotation);
                newCameraPosition = CheckCollisions(cameraPivotPosition, newCameraPosition);

                var lerpValue = _settings.cameraZoomSpeed * Time.deltaTime;
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, newCameraPosition, lerpValue);
                
                cameraTransform.LookAt(cameraPivotPosition);
            }
        }

        private Vector3 CheckCollisions(Vector3 from, Vector3 to)
        {
            if (!_settings.isCollided) return to;

            var hasCollisions = Physics.Linecast(from, to, out var hit, _settings.cameraCollisionMask.value);
            return hasCollisions ? Vector3.Lerp(from, hit.point, 0.85f) : to;
        }

        private Vector3 SphericalCoordinates(Vector2 newCameraRotation)
        {
            newCameraRotation *= Mathf.Deg2Rad;
            var resultPosition = new Vector3
            {
                x = Mathf.Sin(newCameraRotation.y) * Mathf.Sin(newCameraRotation.x),
                z = Mathf.Sin(newCameraRotation.y) * Mathf.Cos(newCameraRotation.x),
                y = Mathf.Cos(newCameraRotation.y)
            };
            
            return resultPosition.normalized * _settings.maxDistanceToTarget;
        }

        private float CheckVerticalBounds(float yValue) => Mathf.Clamp
        (
            yValue,
            _settings.verticalRotationBounds.x,
            _settings.verticalRotationBounds.y
        );
    }
}