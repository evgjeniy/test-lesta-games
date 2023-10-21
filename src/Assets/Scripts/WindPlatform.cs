using System.Collections.Generic;
using Ecs;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
using Ecs.Utilities;
using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WindPlatform : MonoBehaviour
{
    [SerializeField] private ParticleSystem windParticles;
    [SerializeField, Range(0.0f, 10.0f)] private float windSpeed = 1.0f;

    private readonly List<EntityReference> _entityReferences = new();
    private float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        
        if (_elapsedTime > 2.0f)
        {
            _elapsedTime = 0.0f;
            windParticles.transform.Rotate(Vector3.up, Random.Range(0.0f, 360.0f));
        }
        
        foreach (var entityReference in _entityReferences)
        {
            entityReference.Entity.SendMessage(new WindEffectRequest
            {
                windSpeedValue = windSpeed,
                windDirection = windParticles.transform.forward
            });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<EntityReference>(out var entityReference)) return;
        if (!entityReference.Entity.Has<PlayerTag>()) return;
        
        _entityReferences.Add(entityReference);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<EntityReference>(out var entityReference)) return;
        if (!entityReference.Entity.Has<PlayerTag>()) return;

        _entityReferences.Remove(entityReference);
    }
}