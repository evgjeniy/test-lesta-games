using UnityEngine;

namespace Ecs.Components
{
    public class ThirdPersonCameraProvider : Voody.UniLeo.MonoProvider<ThirdPersonCamera> {}

    [System.Serializable]
    public struct ThirdPersonCamera
    {
        [Header("Camera Links")]
        public Camera camera;
        public Transform cameraPivot;

        [HideInInspector] public Vector2 currentRotation;
    }
}