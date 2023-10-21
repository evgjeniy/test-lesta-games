namespace Ecs.Components
{
    public class RigidbodyComponentProvider : Voody.UniLeo.MonoProvider<RigidbodyComponent> {}
    
    [System.Serializable]
    public struct RigidbodyComponent
    {
        public UnityEngine.Rigidbody rigidbody;
    }
}