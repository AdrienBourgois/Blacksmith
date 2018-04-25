using System.Collections;
using Game.Scripts.Entity;
using Game.Scripts.SceneObjects;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DistAttack", menuName = "Attacks/DistAttack")]
    public class SoDistAttack : SoBaseAttack
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
            if (isAttacking || isInCooldown)
                return;

            Debug.Log("ATTACK !");

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<SpriteSceneObject>().location = myAttackEntity.location; //transform.GetChild(0).GetComponent<SceneObject>().location;
            bullet.tag = myAttackEntity.tag;

            TriggerBaseAttack trigger_attack = bullet.GetComponent<TriggerBaseAttack>();
            trigger_attack.damages = damages;
            trigger_attack.onEntityHit += DamageEntity;
            if (isPlayer)
                trigger_attack.onEntityHit += ((PlayerEntity) myAttackEntity).OnEntityHit;

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

        private IEnumerator BulletAddForceCoroutine(SceneObject _bullet, Vector3 _direction)
        {
            while (true)
            {
                _bullet.location += _direction * Time.deltaTime * bulletSpeed;
                yield return null;
            }
        }

        private IEnumerator Cooldown()
        {
            Debug.Log("Cooldown");

            isInCooldown = true;
            float time = coolDown;

            while (time >= 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            isInCooldown = false;
        }
    }
}
