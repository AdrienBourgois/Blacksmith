using Game.Scripts.Triggers;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.Triggers
{
    [CustomEditor(typeof(FightTrigger))]
    public class FightTriggerEditor : UnityEditor.Editor
    {
        [DrawGizmo((GizmoType)19, typeof(FightTrigger))]
        private static void DrawGizmo(FightTrigger _trigger, GizmoType _type)
        {
            if (DebugOptionsWindow.displaySpeechTriggers)
            {
                Gizmos.DrawIcon(_trigger.transform.position, "Game/Fight.png", true);
            }
        }
    }
}
