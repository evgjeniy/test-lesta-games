namespace Ecs.Components.Tags
{
    public class FinishUiTagProvider : Voody.UniLeo.MonoProvider<FinishUiTag> {}

    [System.Serializable]
    public struct FinishUiTag
    {
        public string winUiText;
        public string loseUiText;
        
        [UnityEngine.Range(0.0f, 1.0f)] public float winHColor;
        [UnityEngine.Range(0.0f, 1.0f)] public float loseHColor;
    }
}