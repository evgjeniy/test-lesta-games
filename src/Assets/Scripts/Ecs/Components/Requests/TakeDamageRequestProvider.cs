namespace Ecs.Components.Requests
{
    public class TakeDamageRequestProvider : Voody.UniLeo.MonoProvider<TakeDamageRequest> {}

    [System.Serializable]
    public struct TakeDamageRequest
    {
        public float damageAmount;
    }
}