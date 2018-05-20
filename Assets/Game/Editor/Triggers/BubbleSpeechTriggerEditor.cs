using Game.Scripts.Triggers;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.Triggers
{
    [CustomEditor(typeof(BubbleSpeechTrigger))]
    public class BubbleSpeechTriggerEditor : UnityEditor.Editor
    {
        [DrawGizmo((GizmoType) 19, typeof(BubbleSpeechTrigger))]
        private static void DrawGizmo(BubbleSpeechTrigger _trigger, GizmoType _type)
        {
            if (DebugOptionsWindow.displaySpeechTriggers)
            {
                Gizmos.DrawIcon(_trigger.transform.position, "Game/Bubble.png", true);
            }
        }

    }
}
