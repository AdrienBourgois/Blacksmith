using System;
using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class PhysicSceneObject : SpriteSceneObject
    {
        [Header("Physic Scene Object")]
        public Vector3 velocity;
        public float friction = 5f;
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
            UpdateRuntimeDebug();

            if (Input.GetKey(KeyCode.S))
            {
                velocity.x = -0.2f;
                velocity.y = 0.35f;
            }
        }

        protected virtual void UpdatePhysic()
        {
            if (location.y > 0f)
                currentPhysicState = PhysicState.ON_AIR_UP;
            else if (location.y > 0f)
                currentPhysicState = PhysicState.ON_AIR_DOWN;

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
                    if (velocity.x != 0f)
                        velocity.x = Mathf.Lerp(velocity.x, 0f, Time.deltaTime * friction);
                    if (velocity.z != 0f)
                        velocity.z = Mathf.Lerp(velocity.z, 0f, Time.deltaTime * friction);
                    break;
                case PhysicState.ON_OBJECT:
                    if (velocity.x != 0f)
                        velocity.x = Mathf.Lerp(velocity.x, 0f, Time.deltaTime * friction);
                    if (velocity.z != 0f)
                        velocity.z = Mathf.Lerp(velocity.z, 0f, Time.deltaTime * friction);
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