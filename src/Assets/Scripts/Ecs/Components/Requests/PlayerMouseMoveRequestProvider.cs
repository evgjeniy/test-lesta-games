namespace Ecs.Components.Requests
{
    public class PlayerMouseMoveRequestProvider : Voody.UniLeo.MonoProvider<PlayerMouseMoveRequest> {}

    [System.Serializable]
    public struct PlayerMouseMoveRequest
    {
        public UnityEngine.Vector2 delta;
    }
}