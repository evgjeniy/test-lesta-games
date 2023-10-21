namespace Ecs.Components
{
    public class TransformComponentProvider : Voody.UniLeo.MonoProvider<TransformRef> {}

    [System.Serializable]
    public struct TransformRef
    {
        public UnityEngine.Transform transform;
    }
}