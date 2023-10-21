namespace Ecs.Components
{
    public class HealthViewComponentProvider : Voody.UniLeo.MonoProvider<HealthViewComponent> {}

    [System.Serializable]
    public struct HealthViewComponent
    {
        public UnityEngine.UI.Slider healthBar;
        public TMPro.TMP_Text healthText;
    }
}