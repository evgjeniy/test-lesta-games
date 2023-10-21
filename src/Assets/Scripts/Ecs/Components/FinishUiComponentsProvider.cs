namespace Ecs.Components
{
    public class FinishUiComponentsProvider : Voody.UniLeo.MonoProvider<FinishUiComponents> {}

    [System.Serializable]
    public struct FinishUiComponents
    {
        public UnityEngine.Animator animator;
        
        [UnityEngine.Space]
        public TMPro.TMP_Text resultText;
        public UnityEngine.UI.Image buttonImage;
        public TMPro.TMP_Text buttonText;
    }
}