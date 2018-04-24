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

            /*if (Input.GetKey(KeyCode.UpArrow))
                ListenZAxis(Vector3.forward.z);
                //TryMove(Vector3.forward);
            else if (Input.GetKey(KeyCode.DownArrow))
                ListenZAxis(Vector3.back.z);
                //TryMove(Vector3.back);
            if (Input.GetKey(KeyCode.LeftArrow))
                ListenXAxis(Vector3.left.x);
                //TryMove(Vector3.left);
            else if (Input.GetKey(KeyCode.RightArrow))
                ListenXAxis(Vector3.right.x);
                //TryMove(Vector3.right);
            if (Input.GetKey(KeyCode.Space) && currentPhysicState == PhysicState.ON_GROUND)
                Jump();*/
        }

        protected virtual void ListenXAxis(float _value)
        {
            if (currentPhysicState == PhysicState.ON_GROUND || currentPhysicState == PhysicState.ON_OBJECT)
                TryMove(new Vector3(_value, 0, 0));
        }

        protected virtual void ListenZAxis(float _value)
        {
            if (currentPhysicState == PhysicState.ON_GROUND || currentPhysicState == PhysicState.ON_OBJECT)
                    TryMove(new Vector3(0, 0, _value));
        }

        private void TryMove(Vector3 _move)
        {
            Vector3 new_scale = transform.localScale;
            new_scale.x = Mathf.Abs(new_scale.x);
            new_scale.x *= Mathf.Sign(_move.x);

            Vector3 new_location = location;
            new_location.x += _move.x * Time.deltaTime * speed;
            new_location.z += _move.z * Time.deltaTime * speed;
            if (Physics2D.OverlapPointNonAlloc(location.ToFloor().ToUnitySpace(), floorColliders, 1 << LayerMask.NameToLayer("Floor")) > 0)
            {
                location = new_location;
                transform.localScale = new_scale;
            }
            else
                Debug.Log("Stuck");
        }

        protected virtual void Jump()
        {
            if (currentPhysicState == PhysicState.ON_GROUND || currentPhysicState == PhysicState.ON_OBJECT)
            {
                velocity.y = 0.4f;
                currentPhysicState = PhysicState.ON_AIR_UP;
            }
        }

        protected override void UpdateRuntimeDebug()
        {
            base.UpdateRuntimeDebug();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }
    }
}