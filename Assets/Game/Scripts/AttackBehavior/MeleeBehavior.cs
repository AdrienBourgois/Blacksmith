using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.AttackBehavior
{
    public class MeleeBehavior : MonoBehaviour
    {
        public delegate void TriggerHandler(BaseEntity _entity, float _damages);

        public TriggerHandler onEntityHit;

        private BoxCollider2D collider2_d;
        private BoxCollider2D parent_collider2_d;

        [HideInInspector] public float damages;

        private void Start()
        {
            collider2_d = GetComponent<BoxCollider2D>();
            parent_collider2_d = transform.parent.GetComponent<BoxCollider2D>();

            onEntityHit += transform.parent.GetComponent<BaseEntity>().DamageEntity;
        }

        public void Attack()
        {
            collider2_d = GetComponent<BoxCollider2D>();
            parent_collider2_d = transform.parent.GetComponent<BoxCollider2D>();

            ContactFilter2D filter = new ContactFilter2D {layerMask = 1 << LayerMask.NameToLayer("Entity")};

            filter.useLayerMask = true;

            Collider2D[] colliders = new Collider2D[5];
            collider2_d.OverlapCollider(filter, colliders);

            foreach (Collider2D collide in colliders)
            {
                if (collide == null)
                    continue;

                if (collide != collider2_d && collide != parent_collider2_d && !collide.CompareTag(tag))
                {
                    BaseEntity entity = collide.GetComponent<BaseEntity>();

                    if (onEntityHit != null)
                        onEntityHit(entity, damages);
                }
            }
        }
    }
}
