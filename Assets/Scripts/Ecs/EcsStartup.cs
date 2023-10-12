using Ecs.Systems;
using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

namespace Ecs
{
	public sealed class EcsStartup : MonoBehaviour
	{
		private EcsWorld _world;
		private EcsSystems _systems;

		private void Start()
		{
			_world = new EcsWorld();
			_systems = new EcsSystems(_world).ConvertScene();

			AddSystems();
			AddInjections();

			_systems.Init();
		}

		private void AddSystems()
		{
			_systems
				
				// .Add(new ChangeFpsLimitSystem()
				// .Add(new PlayerInputSystem())
				
				.Add(new DebugMessageSystem());
		}

		private void AddInjections()
		{
			// _systems.Inject(...);
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