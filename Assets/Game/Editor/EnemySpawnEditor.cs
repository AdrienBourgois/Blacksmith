using Game.Editor.SceneObjects;
using Game.Scripts.Triggers;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(EnemySpawn))]
    public class EnemySpawnEditor : SceneObjectEditor
    {
        [DrawGizmo((GizmoType)19, typeof(EnemySpawn))]
        private static void DrawGizmo(EnemySpawn _spawn, GizmoType _type)
        {
            if (DebugOptionsWindow.displayEnemySpawn)
            {
                Gizmos.DrawIcon(_spawn.transform.position, "Game/EnemySpawn.png", true);
            }
        }
    }
}
