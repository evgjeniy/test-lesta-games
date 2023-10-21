using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Tags;
using Ecs.Utilities;
using Leopotam.Ecs;
using Color = UnityEngine.Color;

namespace Ecs.Systems
{
    public class FinishUiSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<LevelEndedEvent> _levelEndedEventFilter;
        private readonly EcsFilter<FinishUiComponents, FinishUiTag> _finishFilter;

        public void Run()
        {
            if (_levelEndedEventFilter.IsEmpty()) return;

            var isPassed = false;
            foreach (var entityId in _levelEndedEventFilter)
            {
                ref var entity = ref _levelEndedEventFilter.GetEntity(entityId);
                isPassed = entity.Get<LevelEndedEvent>().isLevelPassed;
                
                entity.Del<LevelEndedEvent>();
            }
            
            _world.SendMessage<PlayerDisableInputEvent>();
            
            foreach (var entityId in _finishFilter)
            {
                ref var uiComponents = ref _finishFilter.Get1(entityId);
                ref var uiTag = ref _finishFilter.Get2(entityId);
                
                var hColor = isPassed ? uiTag.winHColor : uiTag.loseHColor;

                uiComponents.resultText.text = isPassed ? uiTag.winUiText : uiTag.loseUiText;
                uiComponents.resultText.color = Color.HSVToRGB(hColor, 0.4f, 1.0f);
                uiComponents.buttonImage.color = Color.HSVToRGB(hColor, 0.1f, 1.0f);
                uiComponents.buttonText.color = Color.HSVToRGB(hColor, 0.25f, 1.0f);
                
                uiComponents.animator.SetTrigger(AnimConstants.levelEnded);
            }
        }
    }
}