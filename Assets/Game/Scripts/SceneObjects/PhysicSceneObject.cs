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

        private Collider2D currentObjectCollider;
        private float objectUpperZ;
        private float objectLowerZ;

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

        private void UpdatePhysic()
        {
            if (velocity.y > 0f)
                currentPhysicState = PhysicState.ON_AIR_UP;
            else if (velocity.y > 0f)
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
                    if (TestObjectCollision())
                        OnObjectLand();
                    break;
                case PhysicState.ON_GROUND:
                    UpdateGroundFriction();
                    break;
                case PhysicState.ON_OBJECT:
                    UpdateGroundFriction();
                    UpdateObjectCollision();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateGroundFriction()
        {
            if (velocity.x != 0f)
                velocity.x = Mathf.Lerp(velocity.x, 0f, Time.deltaTime * friction);
            if (velocity.z != 0f)
                velocity.z = Mathf.Lerp(velocity.z, 0f, Time.deltaTime * friction);
            if (velocity.x < 0.01f && velocity.x > 0.01f)
                velocity.x = 0f;
            if (velocity.z < 0.01f && velocity.z > 0.01f)
                velocity.z = 0f;
        }

        private void UpdateCollision()
        {
            if (!IsOnFloorSpace(location + velocity))
            {
                velocity.x = 0f;
                velocity.z = 0f;
            }
        }

        private void UpdatePosition()
        {
            location += velocity;

            if (location.y <= 0f)
            {
                location.y = 0f;
                OnLand();
            }
        }

        private bool TestObjectCollision()
        {
            Collider2D object_collider = Physics2D.OverlapPoint(location.ToUnitySpace(), LayerMask.GetMask("OnObject"));

            if (!object_collider)
                return false;

            float upper_z = object_collider.bounds.max.ToGameSpace().z;
            float lower_z = object_collider.bounds.min.ToGameSpace().z;

            if (!(location.z < upper_z) || !(location.z > lower_z)) return false;

            currentObjectCollider = object_collider;
            objectUpperZ = upper_z;
            objectLowerZ = lower_z;

            return true;

        }

        private void UpdateObjectCollision()
        {
            if (!currentObjectCollider.OverlapPoint(location) || !(location.z < objectUpperZ) || !(location.z > objectLowerZ))
                OnFall();
        }

        private void OnLand()
        {
            velocity.y = 0f;
            currentPhysicState = PhysicState.ON_GROUND;
        }

        private void OnObjectLand()
        {
            velocity.y = 0f;
            currentPhysicState = PhysicState.ON_OBJECT;
        }

        private void OnFall()
        {
            currentPhysicState = PhysicState.ON_AIR_DOWN;
        }
    }
}