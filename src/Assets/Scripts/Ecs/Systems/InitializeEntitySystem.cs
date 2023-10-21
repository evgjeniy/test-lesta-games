using Ecs.Components.Requests;
using Leopotam.Ecs;

namespace Ecs.Systems
{
    public class InitializeEntitySystem : IEcsInitSystem
    {
        private readonly EcsFilter<InitializeEntityRequest> _initializeEntityFilter;
        
        public void Init()
        {
            foreach (var entityId in _initializeEntityFilter)
            {
                ref var entity = ref _initializeEntityFilter.GetEntity(entityId);
                ref var initializeRequest = ref _initializeEntityFilter.Get1(entityId);

                initializeRequest.entityReference.Entity = entity;
            }
        }
    }
}