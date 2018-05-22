using Game.Scripts.Triggers;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.Triggers
{
    [CustomEditor(typeof(EndLevelTrigger))]
    public class EndLevelTriggerEditor : UnityEditor.Editor
    {
        [DrawGizmo((GizmoType)19, typeof(EndLevelTrigger))]
        private static void DrawGizmo(EndLevelTrigger _trigger, GizmoType _type)
        {
            if (DebugOptionsWindow.displayEndLevelTriggers)
            {
                Gizmos.DrawIcon(_trigger.transform.position, "Game/Finish.png", true);
            }
        }
    }
}
