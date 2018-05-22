using Game.Scripts.Entity;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.AttackBehavior
{
    public class MeleeBehavior : MonoBehaviour
    {
        public delegate void TriggerHandler(BaseEntity _entity, SoBaseAttack.HitData hitData);

        public TriggerHandler onEntityHit;

        private BoxCollider2D boxCollider2D;
        private BoxCollider2D parentCollider2D;

        [HideInInspector] public float damages;
        [HideInInspector] public SoBaseAttack.EComboEffect comboEffect;
        [HideInInspector] public float horizontalVelocity;
        [HideInInspector] public float verticalVelocity;

        private void Start()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            parentCollider2D = transform.parent.GetComponent<BoxCollider2D>();

            onEntityHit += transform.parent.GetComponent<BaseEntity>().DamageEntity;
        }

        public void Attack()
        {
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
                        SoBaseAttack.HitData data = new SoBaseAttack.HitData();
                        data.damages = damages;
                        data.comboEffect = comboEffect;
                        data.xDir = transform.parent.localScale.x;
                        data.xVelocity = horizontalVelocity;
                        data.yVelocity = verticalVelocity;

                        onEntityHit(entity, data);
                    }
                }
            }
        }
    }
}
