using Game.Scripts.Entity;
using Game.Scripts.SceneObjects;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.AttackBehavior
{
    public class BulletBehavior : BaseBehavior
    {
        [HideInInspector] public float speed;
        [HideInInspector] public Vector3 direction;

        private void Update()
        {
            location += direction * Time.deltaTime * speed;
        }

        private void OnCollisionEnter2D(Collision2D _other)
        {
            if (!_other.gameObject.CompareTag(tag))
            {
                BaseEntity entity = _other.gameObject.GetComponent<BaseEntity>();
                if (entity != null)
                {
                    hitData.xDir = transform.localScale.x;
                    onEntityHit(entity, hitData);
                    Destroy(gameObject);
                }
            }
        }
    }
}