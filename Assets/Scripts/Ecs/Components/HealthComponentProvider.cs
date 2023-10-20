using UnityEngine;

namespace Ecs.Components
{
    public class HealthComponentProvider : Voody.UniLeo.MonoProvider<HealthComponent> {}

    [System.Serializable]
    public struct HealthComponent
    {
        public float maxHealth;
        [HideInInspector] public float currentHealth;
    }
}