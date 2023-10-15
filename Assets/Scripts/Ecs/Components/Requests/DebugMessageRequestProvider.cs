namespace Ecs.Components.Requests
{
    public class DebugMessageRequestProvider : Voody.UniLeo.MonoProvider<DebugMessageRequest> {}
    
    [System.Serializable]
    public struct DebugMessageRequest
    {
        public UnityEditor.MessageType type; 
        public string message;
    }
}