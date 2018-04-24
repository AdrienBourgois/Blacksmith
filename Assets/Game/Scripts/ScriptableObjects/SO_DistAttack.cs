using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using Game.Scripts.SceneObjects;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DistAttack", menuName = "Attacks/DistAttack")]
    public class SO_DistAttack : SO_BaseAttack
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletLifeTime;

        public override void Init(AttackEntity _my_attack_entity)
        {
            base.Init(_my_attack_entity);
            eAttackType = EAttackType.DISTANCE;
        }

        private void SameTmpAttack() // TMP
        {
            if (isAttacking || isInCooldwnl)
                return;

            Debug.Log("ATTACK !");

            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.GetComponent<SpriteSceneObject>().location = myAttackEntity.location;

            Destroy(bullet, bulletLifeTime);

            myAttackEntity.StartCoroutine(BulletAddForceCoroutine(
                bullet.GetComponent<SpriteSceneObject>(), 
                new Vector3(1 * Mathf.Sign(myAttackEntity.transform.localScale.x), 0, 0)));

            StartCooldown();
        }

        public override void LightGroundedAttack()
        {
            SameTmpAttack();
        }

        public override void HeavyGroundedAttack()
        {
            SameTmpAttack();
        }

        public override void StartCooldown()
        {
            myAttackEntity.StartCoroutine(Cooldown());
        }

        IEnumerator BulletAddForceCoroutine(SpriteSceneObject _bullet, Vector3 _direction)
        {
            while (true)
            {
                _bullet.location += _direction * Time.deltaTime * bulletSpeed;
                yield return null;
            }
        }

        IEnumerator Cooldown()
        {
            Debug.Log("Cooldown");

            isInCooldwnl = true;
            float time = coolDown;

            while (time >= 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            isInCooldwnl = false;
        }
    }
}
