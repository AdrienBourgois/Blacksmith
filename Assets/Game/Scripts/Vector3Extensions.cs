using UnityEngine;

namespace Game.Scripts
{
    public static class Vector3Extensions
    {
        public static Vector3 ToUnitySpace(this Vector3 _vector)
        {
            return new Vector3
            {
                x = _vector.x,
                y = _vector.y + _vector.z * GamePhysic.slope,
                z = _vector.z * GamePhysic.slope
            };
        }

        public static Vector3 ToGameSpace(this Vector3 _vector)
        {
            return new Vector3
            {
                x = _vector.x,
                y = _vector.y - _vector.z * GamePhysic.slope,
                z = _vector.z / GamePhysic.slope
            };
        }

        public static Vector3 ToFloor(this Vector3 _vector)
        {
            return new Vector3
            {
                x = _vector.x,
                y = 0f,
                z = _vector.z,
            };
        }
    }
}