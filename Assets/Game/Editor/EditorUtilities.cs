using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class EditorUtilities : MonoBehaviour
    {
        public static void DrawCollider(PolygonCollider2D _collider, Color _color)
        {
            Vector2 offset = _collider.offset + new Vector2(_collider.gameObject.transform.position.x, _collider.gameObject.transform.position.y);
            for (int i = 0; i < _collider.points.Length - 1; i++)
            {
                Handles.color = _color;
                Handles.DrawLine(_collider.points[i] + offset, _collider.points[i + 1] + offset);
            }
            Handles.DrawLine(_collider.points[0] + offset, _collider.points[_collider.points.Length - 1] + offset);
        }

        public static Color orange = new Color(255f, 153f, 51f);
        public static Color blueMiku = new Color(0f, 255f, 255f);
        public static Color darkGreen = new Color(0f, 102f, 0f);
    }
}
