﻿using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class SceneObject : MonoBehaviour
    {
        [Header("Scene Object")]
        public Vector3 location;
        private const float slope = 0.5f;

        protected virtual void Update()
        {
            UpdateRuntimeDebug();
        }

        protected virtual void LateUpdate()
        {
            SetUnityPosition();
        }

        [ContextMenu("Set Unity Position")]
        public void SetUnityPosition()
        {
            transform.position = location.ToUnitySpace();
        }

        [ContextMenu("Set Game Position")]
        public void SetGamePosition()
        {
            location = transform.position.ToGameSpace();
        }

        protected bool IsOnFloorSpace()
        {
            return GamePhysic.IsPointInCollider(location.ToFloor(), "Floor");
        }

        protected bool IsOnFloorSpace(Vector3 _game_point)
        {
            return GamePhysic.IsPointInCollider(_game_point, "Floor");
        }

        protected bool IsInCameraSpace()
        {
            return GamePhysic.IsPointInCollider(location.ToFloor(), "Camera");
        }

        protected bool IsInCameraSpace(Vector3 _game_point)
        {
            return GamePhysic.IsPointInCollider(_game_point, "Camera");
        }

        protected virtual void UpdateRuntimeDebug()
        {

        }

        protected virtual void OnDrawGizmos()
        {
            Debug.DrawLine(location.ToUnitySpace(), location.ToFloor().ToUnitySpace(), Color.blue);
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(location.ToFloor().ToUnitySpace(), Vector3.forward, 0.15f);
        }
    }
}