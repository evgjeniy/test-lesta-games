using Ecs.Components;
using Ecs.Components.Requests;
using Ecs.Components.Tags;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems
{
    public class PlayerHealthSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, PlayerTag> _allPlayersFilter;
        private readonly EcsFilter<HealthComponent, HealRequest> _healedFilter;
        private readonly EcsFilter<HealthComponent, TakeDamageRequest> _damagedFilter;

        private readonly PlayerSettings _settings;
        
        public void Init()
        {
            foreach (var entityId in _allPlayersFilter)
            {
                ref var playerHealthComponent = ref _allPlayersFilter.Get1(entityId);
                playerHealthComponent.currentHealth = _settings.playerMaxHealth;
            }
        }

        public void Run()
        {
            foreach (var entityId in _healedFilter)
            {
                ref var healthComponent = ref _healedFilter.Get1(entityId);
                healthComponent.currentHealth -= _healedFilter.Get2(entityId).healAmount;
            }
            
            foreach (var entityId in _damagedFilter)
            {
                ref var healthComponent = ref _damagedFilter.Get1(entityId);
                healthComponent.currentHealth -= _damagedFilter.Get2(entityId).damageAmount;
            }
        }
    }
}