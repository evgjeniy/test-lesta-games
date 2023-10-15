namespace Ecs.Components.Requests
{
    public class InitializeEntityRequestProvider : Voody.UniLeo.MonoProvider<InitializeEntityRequest> {}

    [System.Serializable]
    public struct InitializeEntityRequest
    {
        public EntityReference entityReference;
    }
}