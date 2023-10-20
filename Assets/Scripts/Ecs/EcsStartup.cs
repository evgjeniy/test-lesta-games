using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Systems;
using UnityEngine;
using Leopotam.Ecs;
using ScriptableObjects;
using Voody.UniLeo;

namespace Ecs
{
	public sealed class EcsStartup : MonoBehaviour
	{
		[SerializeField] private PlayerSettings playerSettings;
		
		private EcsWorld _world;
		private EcsSystems _systems;
		
		private void Start()
		{
			_world = new EcsWorld();
			_systems = new EcsSystems(_world).ConvertScene();

			AddSystems();
			AddOneFrames();

			_systems.Inject(playerSettings);
			_systems.Init();
		}

		private void AddSystems()
		{
			// _systems.Add(new ChangeFpsLimitSystem()

			_systems.Add(new InitializeEntitySystem());
			
			_systems.Add(new PlayerInputSystem());
			_systems.Add(new PlayerJumpSystem());
			_systems.Add(new PlayerThirdPersonMovementSystem());
			_systems.Add(new PlayerThirdPersonMouseLookSystem());
			_systems.Add(new PlayerAnimationSystem());
			_systems.Add(new PlayerHealthSystem());
			_systems.Add(new HealthViewSystem());
			_systems.Add(new FinishUiSystem());
			_systems.Add(new DebugMessageSystem());
		}

		private void AddOneFrames()
		{
			_systems.OneFrame<InitializeEntityRequest>();
			_systems.OneFrame<PlayerJumpEvent>();
			_systems.OneFrame<TrampolineJumpRequest>();
			_systems.OneFrame<PlayerMovementRequest>();
			_systems.OneFrame<PlayerMouseMoveRequest>();
			_systems.OneFrame<HealRequest>();
			_systems.OneFrame<TakeDamageRequest>();
			_systems.OneFrame<HealthChangedEvent>();
			_systems.OneFrame<WindEffectRequest>();
			_systems.OneFrame<LevelEndedEvent>();
			_systems.OneFrame<DebugMessageRequest>();
		}

		private void Update() => _systems?.Run();

		private void OnDestroy()
		{
			_systems?.Destroy();
			_systems = null;
			_world?.Destroy();
			_world = null;
		}
	}
}