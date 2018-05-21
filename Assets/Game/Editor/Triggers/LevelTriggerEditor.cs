using Game.Scripts.Triggers;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.Triggers
{
    [CustomEditor(typeof(LevelTrigger))]
    public class LevelTriggerEditor : UnityEditor.Editor
    {
        [DrawGizmo((GizmoType)34, typeof(LevelTrigger))]
        private static void DrawGizmo(LevelTrigger _trigger, GizmoType _type)
        {
            if (DebugOptionsWindow.displayTriggerZones)
                foreach (PolygonCollider2D polygon in _trigger.GetComponents<PolygonCollider2D>())
                    EditorUtilities.DrawCollider(polygon, Color.magenta);
        }
    }
}
