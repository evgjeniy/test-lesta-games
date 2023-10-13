using UnityEngine;

namespace Ecs.Components
{
    public class JumpComponentProvider : Voody.UniLeo.MonoProvider<JumpComponent> {}

    [System.Serializable]
    public struct JumpComponent
    {
        public Transform triggerCenter;
        public float triggerRadius;
        public LayerMask triggerMask;

        [HideInInspector] public bool isGrounded;
    }
}