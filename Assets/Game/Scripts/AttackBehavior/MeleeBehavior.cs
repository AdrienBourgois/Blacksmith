using Game.Scripts.Entity;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.AttackBehavior
{
    public class MeleeBehavior : BaseBehavior
    {
        private BoxCollider2D boxCollider2D;
        private BoxCollider2D parentCollider2D;

        private void Start()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            parentCollider2D = transform.parent.GetComponent<BoxCollider2D>();

            onEntityHit += transform.parent.GetComponent<BaseEntity>().DamageEntity;
        }

        private void Update()
        {
            location.y = transform.parent.position.ToGameSpace().y + 1f;
            location.x = transform.parent.position.ToGameSpace().x + 1f * transform.parent.localScale.x;
            location.z = transform.parent.position.ToGameSpace().z;
        }

        public void Attack()
        {
            gameObject.SetActive(true);

            boxCollider2D = GetComponent<BoxCollider2D>();
            parentCollider2D = transform.parent.GetComponent<BoxCollider2D>();

            ContactFilter2D filter = new ContactFilter2D {layerMask = 1 << LayerMask.NameToLayer("Entity")};

            filter.useLayerMask = true;

            Collider2D[] colliders = new Collider2D[5];
            boxCollider2D.OverlapCollider(filter, colliders);

            foreach (Collider2D collide in colliders)
            {
                if (collide == null)
                    continue;

                if (collide != boxCollider2D && collide != parentCollider2D && !collide.CompareTag(tag))
                {
                    BaseEntity entity = collide.GetComponent<BaseEntity>();

                    if (onEntityHit != null)
                    {
                        hitData.xDir = transform.parent.localScale.x;
                        onEntityHit(entity, hitData);
                    }
                }
            }
        }
    }
}
