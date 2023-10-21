using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
using Ecs.Utilities;
using Leopotam.Ecs;

namespace Ecs.Systems
{
    public class PlayerHealthSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<HealthComponent, PlayerTag> _allPlayersFilter;
        private readonly EcsFilter<HealthComponent, HealRequest> _healedFilter;
        private readonly EcsFilter<HealthComponent, TakeDamageRequest> _damagedFilter;
        
        public void Init()
        {
            foreach (var entityId in _allPlayersFilter)
            {
                ref var entity = ref _allPlayersFilter.GetEntity(entityId);
                ref var playerHealthComponent = ref _allPlayersFilter.Get1(entityId);
                playerHealthComponent.currentHealth = playerHealthComponent.maxHealth;
                
                entity.SendMessage<HealthChangedEvent>();
            }
        }

        public void Run()
        {
            foreach (var entityId in _healedFilter)
            {
                ref var entity = ref _damagedFilter.GetEntity(entityId);
                ref var healthComponent = ref _healedFilter.Get1(entityId);
                healthComponent.currentHealth -= _healedFilter.Get2(entityId).healAmount;
                
                entity.SendMessage<HealthChangedEvent>();
            }
            
            foreach (var entityId in _damagedFilter)
            {
                ref var entity = ref _damagedFilter.GetEntity(entityId);
                ref var healthComponent = ref _damagedFilter.Get1(entityId);
                healthComponent.currentHealth -= _damagedFilter.Get2(entityId).damageAmount;

                entity.SendMessage<HealthChangedEvent>();
                
                if (healthComponent.currentHealth <= 0.0f)
                    _world.SendMessage<LevelEndedEvent>();
            }
        }
    }
}