using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class MovablePhysicSceneObject : PhysicSceneObject
    {
        [Header("Movable Physic Scene Object")]
        public float speed = 3f;

        protected override void Update()
        {
            base.Update();

            if (Input.GetKey(KeyCode.UpArrow))
                TryMove(Vector3.forward);
            else if (Input.GetKey(KeyCode.DownArrow))
                TryMove(Vector3.back);
            if (Input.GetKey(KeyCode.LeftArrow))
                TryMove(Vector3.left);
            else if (Input.GetKey(KeyCode.RightArrow))
                TryMove(Vector3.right);
            if (Input.GetKey(KeyCode.Space) && currentPhysicState == PhysicState.ON_GROUND)
                Jump();
        }

        private void TryMove(Vector3 _move)
        {
            Vector3 new_location = location;
            new_location.x += _move.x * Time.deltaTime * speed;
            new_location.z += _move.z * Time.deltaTime * speed;
            if (Physics2D.OverlapPointNonAlloc(location.ToFloor().ToUnitySpace(), floorColliders, 1 << LayerMask.NameToLayer("Floor")) > 0)
                location = new_location;
        }

        private void Jump()
        {
            velocity.y = 0.4f;
            currentPhysicState = PhysicState.ON_AIR_UP;
        }
    }
}