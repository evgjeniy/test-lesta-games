﻿using Ecs.Components;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
using Ecs.Utilities;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMovementRequest> _movementRequests = null;
        private readonly EcsFilter<AnimatorRef, PlayerTag> _animationFilter;
        private readonly PlayerSettings _settings;

        public void Run()
        {
            foreach (var entityId in _animationFilter)
            {
                ref var entity = ref _animationFilter.GetEntity(entityId);
                ref var animator = ref entity.Get<AnimatorRef>().animator;

                if (entity.Has<JumpComponent>())
                    animator.SetBool(AnimConstants.isJumping, !entity.Get<JumpComponent>().isGrounded);

                var oldSpeed = animator.GetFloat(AnimConstants.runSpeed);
                var newSpeed = _movementRequests.IsEmpty()
                    ? 0
                    : 1 / (_movementRequests.Get1(0).isRunning ? 1.0f : _settings.runMultiplier);
                
                var lerpSpeed = Mathf.Lerp(oldSpeed, newSpeed, _settings.runAnimationSmoothTime * Time.deltaTime);

                animator.SetFloat(AnimConstants.runSpeed, lerpSpeed);
            }
        }
    }
}