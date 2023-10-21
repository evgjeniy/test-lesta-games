using System.Collections.Generic;
using Ecs;
using Ecs.Components;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
using Ecs.Utilities;
using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class TrampolinePlatform : MonoBehaviour
{
    [SerializeField, Range(0.0f, 30.0f)] private float jumpForce = 15.0f;

    private readonly List<EntityReference> _entityReferences = new();
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.TryGetComponent<EntityReference>(out var entityReference)) return;
        if (!entityReference.Entity.Has<PlayerTag>()) return;

        _entityReferences.Add(entityReference);
        _animator.SetTrigger(AnimConstants.trampolineJump);
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.collider.TryGetComponent<EntityReference>(out var entityReference)) return;
        if (!entityReference.Entity.Has<PlayerTag>()) return;

        _entityReferences.Remove(entityReference);
    }

    public void JumpEvent()
    {
        foreach (var entityReference in _entityReferences)
        {
            if (entityReference is null) return;
            if (entityReference.Entity.Has<HealthComponent>() is false) return;

            entityReference.Entity.SendMessage(new TrampolineJumpRequest { jumpForce = jumpForce });
        }
    }
}