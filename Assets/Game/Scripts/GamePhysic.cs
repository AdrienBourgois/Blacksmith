using UnityEngine;

namespace Game.Scripts
{
    public static class GamePhysic
    {
        public const float slope = 0.5f;

        public static Vector3 zAxis = new Vector3(0f, 1f - slope, slope);

        public static Plane zPlane = new Plane(Vector3.zero, Vector3.right, zAxis);

        public static float Gravity
        {
            get { return Physics2D.gravity.y; }
        }

        private static readonly Collider2D[] tempColliderArray = new Collider2D[1];
        public static bool IsPointInCollider(Vector3 _game_point, string _layer)
        {
            return Physics2D.OverlapPointNonAlloc(_game_point.ToUnitySpace(), tempColliderArray, 1 << LayerMask.NameToLayer(_layer)) > 0;
        }

        public static bool IsPointInColliderDepth(Vector3 _location, Collider2D _collider, float _max_depth)
        {
            if (!_collider.OverlapPoint(_location.ToUnitySpace())) return false;

            return Mathf.Abs(_collider.transform.position.ToGameSpace().z - _location.z) < _max_depth;
        }
    }
}