namespace Ecs.Components.Requests
{
    public class HealRequestProvider : Voody.UniLeo.MonoProvider<HealRequest> {}

    [System.Serializable]
    public struct HealRequest
    {
        public float healAmount;
    }
}