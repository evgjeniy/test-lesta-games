using System.Collections.Generic;
using Ecs;
using Ecs.Components.Events;
using Ecs.Components.Tags;
using Ecs.Utilities;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

[RequireComponent(typeof(Collider))]
public class ReverseMovementPlatform : MonoBehaviour
{
    private readonly List<EntityReference> _entityReferences = new();

    private void Update()
    {
        if (_entityReferences.Count == 0) return;
        
        WorldHandler.GetWorld().SendMessage<ReverseMovementEvent>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.TryGetComponent<EntityReference>(out var entityReference)) return;
        if (!entityReference.Entity.Has<PlayerTag>()) return;
        
        _entityReferences.Add(entityReference);
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (!other.collider.TryGetComponent<EntityReference>(out var entityReference)) return;
        if (!entityReference.Entity.Has<PlayerTag>()) return;
        
        _entityReferences.Remove(entityReference);
    }
}