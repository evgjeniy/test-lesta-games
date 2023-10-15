using UnityEngine;

namespace Ecs.Utilities
{
    public static class Constants
    {
        public static readonly Vector2 normalizedMoveSpeed = new(0.2f, 1.0f);
    }
    
    public static class AnimConstants
    {
        public static readonly int runSpeed = Animator.StringToHash("Run Speed");
        public static readonly int isJumping = Animator.StringToHash("Is Jumping");
        public static readonly int takeDamage = Animator.StringToHash("Take Damage");
        public static readonly int trampolineJump = Animator.StringToHash("Trampoline Jump");
    }
}