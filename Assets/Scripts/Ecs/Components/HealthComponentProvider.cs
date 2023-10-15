using UnityEngine;

namespace Ecs.Components
{
    public class HealthComponentProvider : Voody.UniLeo.MonoProvider<HealthComponent> {}

    [System.Serializable]
    public struct HealthComponent
    {
        [HideInInspector] public float currentHealth;
    }
}