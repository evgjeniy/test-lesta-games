using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Settings", fileName = "Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Movement Settings")]
        [Range(0.0f, 10.0f)] public float moveSpeed = 2.0f;
        [Range(1.0f, 5.0f)] public float runMultiplier = 2.0f;
        [Range(0.0f, 10.0f)] public float rotationSmoothSpeed = 7.0f;
        [Range(0.0f, 10.0f)] public float runAnimationSmoothTime = 7.0f;

        [Header("Jump Settings")]
        [Range(0.0f, 10.0f)] public float jumpForce = 7.0f;
        
        [Min(0.0f)] public Vector2 cameraSensitivity = new(1.0f, 1.0f);
    }
}