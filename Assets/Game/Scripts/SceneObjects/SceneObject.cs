using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class SceneObject : MonoBehaviour
    {
        [Header("Scene Object")]
        [HideInInspector]
        public Vector3 location;

        protected virtual void Start()
        {}

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
            transform.localPosition = location.ToUnitySpace();
        }

        [ContextMenu("Set Game Position")]
        public void SetGamePosition()
        {
            location = transform.position.ToGameSpace();
        }

        public void ToFloor()
        {
            location.y = 0f;
            SetUnityPosition();
        }

        public bool IsOnFloorSpace()
        {
            return GamePhysic.IsPointInCollider(location.ToFloor(), "Floor");
        }

        protected bool IsOnFloorSpace(Vector3 _game_point)
        {
            return GamePhysic.IsPointInCollider(_game_point, "Floor");
        }

        public bool IsInCameraSpace()
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
    }
}