using Game.Scripts.AttackBehavior;
using Game.Scripts.Entity;
using Game.Scripts.SceneObjects;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "RangeAttack", menuName = "Attacks/Range")]
    public class SoRangeAttack : SoBaseAttack
    {
        [SerializeField] private GameObject bulletPrefab;

        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletLifeTime;

        public override void Attack(BaseEntity _entity)
        {
            base.Attack(_entity);

            GameObject bullet_pref = Instantiate(bulletPrefab, _entity.transform.root);
            bullet_pref.transform.localScale = _entity.transform.localScale;// new Vector3(_entity.GetXScale(), 1, 1);
            bullet_pref.transform.rotation = _entity.transform.rotation;
            Destroy(bullet_pref, bulletLifeTime);

            BulletBehavior bullet = bullet_pref.GetComponent<BulletBehavior>();

            bullet.location = _entity.transform.GetChild(0).position.ToGameSpace();
            bullet.tag = _entity.tag;
            bullet.damages = damages;
            bullet.speed = bulletSpeed;

            bullet.direction = _entity.transform.rotation * new Vector3(_entity.transform.localScale.x/*1 * _entity.GetXScale()Mathf.Sign(_entity.transform.parent.localScale.x)*/, 0, 0);
            bullet.direction.z = bullet.direction.y;
            bullet.direction.y = 0;
            bullet.onEntityHit += _entity.DamageEntity;
        }
    }
}