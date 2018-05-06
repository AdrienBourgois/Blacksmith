using System;
using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class PhysicSceneObject : SpriteSceneObject
    {
        [Header("Physic Scene Object")]
        public Vector3 velocity;
        public float friction = 5f;
        public float weight = 0.2f;
        protected PhysicState currentPhysicState = PhysicState.ON_GROUND;

        protected enum PhysicState
        {
            ON_AIR_UP,
            ON_AIR_DOWN,
            ON_GROUND,
            ON_OBJECT
        }

        protected virtual void FixedUpdate()
        {
            UpdatePhysic();
            UpdateCollision();
            UpdatePosition();
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
                    velocity.y = Mathf.Lerp(velocity.y, GamePhysic.Gravity, Time.deltaTime * weight);
                    if (velocity.y < 0f)
                        currentPhysicState = PhysicState.ON_AIR_DOWN;
                    break;
                case PhysicState.ON_AIR_DOWN:
                    velocity.y = Mathf.Lerp(velocity.y, GamePhysic.Gravity, Time.deltaTime * weight);
                    break;
                case PhysicState.ON_GROUND:
                case PhysicState.ON_OBJECT:
                    if (velocity.x != 0f)
                        velocity.x = Mathf.Lerp(velocity.x, 0f, Time.deltaTime * friction);
                    if (velocity.z != 0f)
                        velocity.z = Mathf.Lerp(velocity.z, 0f, Time.deltaTime * friction);
                    if (velocity.x < 0.01f && velocity.x > 0.01f)
                        velocity.x = 0f;
                    if (velocity.z < 0.01f && velocity.z > 0.01f)
                        velocity.z = 0f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void UpdateCollision()
        {
            if (!IsOnFloorSpace(location + velocity))
            {
                velocity.x = 0f;
                velocity.z = 0f;
            }
        }

        protected virtual void UpdatePosition()
        {
            location += velocity;

            if (location.y <= 0f)
            {
                location.y = 0f;
                OnLand();
            }
        }

        protected virtual void OnLand()
        {
            velocity.y = 0f;
            currentPhysicState = PhysicState.ON_GROUND;
        }
    }
}