namespace Ecs.Components.Events
{
    public class LevelEndedEventProvider : Voody.UniLeo.MonoProvider<LevelEndedEvent> {}

    [System.Serializable]
    public struct LevelEndedEvent
    {
        public bool isLevelPassed;
    }
}