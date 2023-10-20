using Ecs;
using Ecs.Components.Events;
using Ecs.Components.Tags;
using Ecs.Utilities;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

[RequireComponent(typeof(Collider))]
public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private bool isPassed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<EntityReference>(out var entityReference)) return;
        if (!entityReference.Entity.Has<PlayerTag>()) return;

        var ecsWorld = WorldHandler.GetWorld();

        ecsWorld.SendMessage(new LevelEndedEvent { isLevelPassed = isPassed });
    }
}