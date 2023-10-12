using UnityEditor;

namespace Ecs.Components.Requests
{
    public class DebugMessageRequestProvider : Voody.UniLeo.MonoProvider<DebugMessageRequest> {}
    
    [System.Serializable]
    public struct DebugMessageRequest
    {
        public MessageType type; 
        public string message;
    }
}