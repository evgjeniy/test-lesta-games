using Ecs.Components;
using Ecs.Components.Events;
using Leopotam.Ecs;

namespace Ecs.Systems
{
    public class HealthViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, HealthViewComponent, HealthChangedEvent> _healthFilter;
        
        public void Run()
        {
            foreach (var entityId in _healthFilter)
            {
                ref var health = ref _healthFilter.Get1(entityId);
                ref var healthView = ref _healthFilter.Get2(entityId);

                healthView.healthBar.value = health.currentHealth / health.maxHealth;
                healthView.healthText.text = $"HP: {health.currentHealth}";
            }
        }
    }
}