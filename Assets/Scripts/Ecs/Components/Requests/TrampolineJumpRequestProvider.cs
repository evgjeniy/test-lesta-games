namespace Ecs.Components.Requests
{
    public class TrampolineJumpRequestProvider : Voody.UniLeo.MonoProvider<TrampolineJumpRequest> {}

    [System.Serializable]
    public struct TrampolineJumpRequest
    {
        public float jumpForce;
    }
}