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
    [SerializeField, Range(0.0f, 10.0f)] private float windSpeed = 0.7f;

    private readonly List<EntityReference> _entityReferences = new();
    private float _elapsedTime;
    private Vector3 _windDirection;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        
        if (_elapsedTime > 2.0f)
        {
            _elapsedTime = 0.0f;
            _windDirection = GetRandomWindDirection();
            windParticles.transform.LookAt(_windDirection);
        }
        
        foreach (var entityReference in _entityReferences)
        {
            entityReference.Entity.SendMessage(new WindEffectRequest
            {
                windSpeedValue = windSpeed,
                windDirection = _windDirection
            });
        }
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

    private static Vector3 GetRandomWindDirection() => new Vector3
    {
        x = Random.Range(-1.0f, 1.0f), 
        y = 0.0f, 
        z = Random.Range(-1.0f, 0.0f)
    }.normalized;
}