using System;
using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class PhysicSceneObject : SpriteSceneObject
    {
        [Header("Physic Scene Object")]
        //Physic
        public Vector3 velocity;
        protected const float friction = 0.1f;
        protected PhysicState currentPhysicState = PhysicState.ON_GROUND;

        protected readonly Collider2D[] floorColliders = new Collider2D[3];

        protected enum PhysicState
        {
            ON_AIR_UP,
            ON_AIR_DOWN,
            ON_GROUND,
            ON_OBJECT,
        }

        protected override void Update()
        {
            base.Update();

            UpdatePhysic();
            UpdateDebug();
        }

        protected virtual void UpdatePhysic()
        {
            switch (currentPhysicState)
            {
                case PhysicState.ON_AIR_UP:
                    velocity.y = Mathf.Lerp(velocity.y, GamePhysic.Gravity, Time.deltaTime / 5f);
                    if (velocity.y < 0f)
                        currentPhysicState = PhysicState.ON_AIR_DOWN;
                    break;
                case PhysicState.ON_AIR_DOWN:
                    velocity.y = Mathf.Lerp(velocity.y, GamePhysic.Gravity, Time.deltaTime / 5f);
                    break;
                case PhysicState.ON_GROUND:
                    break;
                case PhysicState.ON_OBJECT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            location += velocity;

            if (location.y <= 0f)
            {
                location.y = 0f;
                OnLand();
            }
        }

        protected void OnLand()
        {
            velocity.y = 0f;
            currentPhysicState = PhysicState.ON_GROUND;
        }

        private void UpdateDebug()
        {

        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Debug.DrawLine(location.ToUnitySpace(), location.ToFloor().ToUnitySpace(), Color.blue);
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(location.ToFloor().ToUnitySpace(), Vector3.forward, 0.15f);
        }
    }
}