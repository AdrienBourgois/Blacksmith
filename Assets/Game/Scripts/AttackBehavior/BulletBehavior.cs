using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using Game.Scripts.Interfaces;
using Game.Scripts.SceneObjects;
using UnityEngine;

namespace Game.Scripts.AttackBehavior
{
    public class BulletBehavior : SpriteSceneObject
    {
        public delegate void TriggerHandler(BaseEntity _entity, float _damages);

        public TriggerHandler onEntityHit;

        [HideInInspector] public Vector3 direction;
        [HideInInspector] public float speed;
        [HideInInspector] public float damages;

        protected override void Update()
        {
            base.Update();

            location += direction * Time.deltaTime * speed;
        }

        private void OnCollisionEnter2D(Collision2D _other)
        {
            if (!_other.gameObject.CompareTag(tag))
            {
                BaseEntity entity = _other.gameObject.GetComponent<BaseEntity>();
                if (entity != null)
                {
                    onEntityHit(entity, damages);
                    Destroy(gameObject);
                }
            }
        }
    }
}