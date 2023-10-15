using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Settings", fileName = "Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Movement Settings")]
        [Range(0.0f, 10.0f)] public float moveSpeed = 1.5f;
        [Range(1.0f, 10.0f)] public float runMultiplier = 3.0f;
        [Range(0.0f, 30.0f)] public float rotationSmoothSpeed = 10.0f;
        [Range(0.0f, 30.0f)] public float runAnimationSmoothTime = 10.0f;

        [Header("Jump Settings")]
        [Range(0.0f, 30.0f)] public float jumpForce = 6.0f;
        
        [Header("Camera Settings")]
        [Min(0.0f)] public Vector2 verticalRotationBounds = new(10.0f, 130.0f);
        [Min(0.0f)] public Vector2 cameraSensitivity = new(5.0f, 5.0f);
        [Range(0.0f, 30.0f)] public float maxDistanceToTarget = 5.0f;
        [Range(0.0f, 30.0f)] public float cameraZoomSpeed = 15.0f;

        [Header("Camera collision detection")]
        public bool isCollided = true;
        public LayerMask cameraCollisionMask;

        [Header("Player Settings")]
        [Min(0.0f)] public float playerMaxHealth = 100.0f;
    }
}