namespace Ecs.Components
{
    public class AnimatorComponentProvider : Voody.UniLeo.MonoProvider<AnimatorRef> {}

    [System.Serializable]
    public struct AnimatorRef
    {
        public UnityEngine.Animator animator;
    }
}