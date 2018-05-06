﻿using System.Collections;
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
            Destroy(bullet_pref, bulletLifeTime);

            BulletBehavior bullet = bullet_pref.GetComponent<BulletBehavior>();

            bullet.location = _entity.transform.GetChild(0).GetComponent<SceneObject>().transform.position.ToGameSpace();
            bullet.tag = _entity.tag;
            bullet.damages = damages;
            bullet.speed = bulletSpeed;
            bullet.direction = new Vector3(1 * Mathf.Sign(_entity.transform.localScale.x), 0, 0);
            bullet.onEntityHit += _entity.DamageEntity;
        }
    }
}