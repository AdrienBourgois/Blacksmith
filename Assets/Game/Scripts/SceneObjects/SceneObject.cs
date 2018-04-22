using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class SceneObject : MonoBehaviour
    {
        [Header("Scene Object")]
        public Vector3 location;
        private const float slope = 0.5f;

        protected virtual void Awake()
        {
        }

        protected virtual void Update()
        {
        }

        protected virtual void LateUpdate()
        {
            SetPosition();
        }

        protected virtual void OnValidate()
        {

        }

        public void SetPosition()
        {
            transform.position = location.ToUnitySpace();
        }
    }
}