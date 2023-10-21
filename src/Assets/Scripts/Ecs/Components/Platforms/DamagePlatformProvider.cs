namespace Ecs.Components.Platforms
{
    public class DamagePlatformProvider : Voody.UniLeo.MonoProvider<DamagePlatform> {}

    [System.Serializable]
    public struct DamagePlatform
    {
        public float activationDelay;
        public float damageAmount;
    }
}