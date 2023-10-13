using System;

namespace Ecs.Components.Requests
{
    public class PlayerMovementRequestProvider : Voody.UniLeo.MonoProvider<PlayerMovementRequest> {}

    [Serializable]
    public struct PlayerMovementRequest
    {
        public UnityEngine.Vector3 moveDirection;
        public bool isRunning;
    }
}