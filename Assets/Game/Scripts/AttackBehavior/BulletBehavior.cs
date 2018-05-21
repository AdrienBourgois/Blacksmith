using Game.Scripts.Entity;
using Game.Scripts.SceneObjects;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.AttackBehavior
{
    public class BulletBehavior : SpriteSceneObject
    {
        public delegate void TriggerHandler(BaseEntity _entity, SoBaseAttack.HitData _data);

        public TriggerHandler onEntityHit;

        [HideInInspector] public Vector3 direction;
        [HideInInspector] public float speed;
        [HideInInspector] public float damages;
        [HideInInspector] public SoBaseAttack.EComboEffect comboEffect;
        [HideInInspector] public float horizontalVelocity;
        [HideInInspector] public float verticalVelocity;
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
                    SoBaseAttack.HitData data = new SoBaseAttack.HitData();
                    data.damages = damages;
                    data.comboEffect = comboEffect;
                    data.xDir = transform.localScale.x;
                    data.xVelocity = horizontalVelocity;
                    data.yVelocity = verticalVelocity;

                    onEntityHit(entity, data);
                    Destroy(gameObject);
                }
            }
        }
    }
}