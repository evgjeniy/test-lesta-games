namespace Ecs.Components
{
    public class RigidbodyComponentProvider : Voody.UniLeo.MonoProvider<RigidbodyRef> {}
    
    [System.Serializable]
    public struct RigidbodyRef
    {
        public UnityEngine.Rigidbody rigidbody;
    }
}