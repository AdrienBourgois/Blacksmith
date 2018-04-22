using UnityEngine;

namespace Game.Scripts
{
    public static class GamePhysic
    {
        public static float Gravity
        {
            get { return Physics2D.gravity.y; }
        }

        public const float slope = 0.5f;

        public static Vector3 zAxis = new Vector3(0f, 1f - slope, slope);
    }
}