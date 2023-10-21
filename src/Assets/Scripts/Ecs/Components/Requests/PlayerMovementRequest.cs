namespace Ecs.Components.Requests
{
    public class PlayerMovementRequestProvider : Voody.UniLeo.MonoProvider<PlayerMovementRequest> {}

    [System.Serializable]
    public struct PlayerMovementRequest
    {
        public UnityEngine.Vector2 direction;
        public bool isRunning;
    }
}