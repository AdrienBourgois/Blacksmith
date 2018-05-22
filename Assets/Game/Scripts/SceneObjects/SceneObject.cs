using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class SceneObject : MonoBehaviour
    {
        [Header("Scene Object")]
        [Tooltip("Avoid changing this location here, use the SceneObject Editor window !")]
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
            transform.position = location.ToUnitySpace();
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

        public bool DoesPointOverlapDepth(Vector3 _point, float _depth)
        {
            Vector3 unity_point = _point.ToUnitySpace();

            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
            if (sprite_renderer)
            {
                if (sprite_renderer.bounds.Contains(unity_point))
                    return Mathf.Abs(_point.z - location.z) < _depth;
            }

            Collider2D collider2_d = GetComponent<Collider2D>();
            if (collider2_d)
            {
                if (collider2_d.OverlapPoint(unity_point))
                    return Mathf.Abs(_point.z - location.z) < _depth;
            }
            return false;
        }

        public SceneObject[] GetObjectsInLayer(float _depth, LayerMask _mask)
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll((location + Vector3.forward * (_depth)).ToUnitySpace(), _mask);
            Collider2D[] colliders_up = Physics2D.OverlapPointAll((location + Vector3.forward * (_depth / 2f)).ToUnitySpace(), _mask);
            colliders = colliders.Concat(colliders_up).ToArray();
            colliders = colliders.Distinct().ToArray();
            if (colliders.Length > 0)
                return colliders.Select(_col => _col.GetComponent<SceneObject>()).Where(_scene_object => Mathf.Abs(_scene_object.location.z - location.z) < _depth).ToArray();

            return null;
        }

        public bool IsInRadius(Vector3 _point, float _radius)
        {
            return (location - _point).magnitude < _radius;
        }

        protected virtual void UpdateRuntimeDebug()
        {

        }
    }
}