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
public class DamagePlatform : MonoBehaviour
{
    [SerializeField] private float damage = 20.0f;

    private readonly List<EntityReference> _entityReferences = new();
    private Animator _platformAnimator;

    private void Awake() => _platformAnimator = GetComponent<Animator>();

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.TryGetComponent<EntityReference>(out var entityReference)) return;
        if (!entityReference.Entity.Has<PlayerTag>()) return;

        _platformAnimator.SetTrigger(AnimConstants.takeDamage);

        _entityReferences.Add(entityReference);
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.collider.TryGetComponent<EntityReference>(out var entityReference)) return;
        if (!entityReference.Entity.Has<PlayerTag>()) return;

        _entityReferences.Remove(entityReference);
    }

    public void DamageEvent()
    {
        foreach (var entityReference in _entityReferences)
        {
            if (entityReference is null) return;
            if (entityReference.Entity.Has<HealthComponent>() is false) return;

            entityReference.Entity.SendMessage(new TakeDamageRequest { damageAmount = damage });
        }
    }
}