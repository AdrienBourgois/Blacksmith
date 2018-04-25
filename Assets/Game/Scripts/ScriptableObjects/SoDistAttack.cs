using System.Collections;
using Game.Scripts.Entity;
using Game.Scripts.SceneObjects;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DistAttack", menuName = "Attacks/DistAttack")]
    public class SoDistAttack : SoBaseAttack
    {
        [SerializeField] private GameObject weakBulletPrefab;
        [SerializeField] private GameObject heavyBulletPrefab;


        [SerializeField] private float weakBulletSpeed;
        [SerializeField] private float heavyBulletSpeed;

        [SerializeField] private float bulletLifeTime;

        public override void Init(AttackEntity _my_attack_entity)
        {
            base.Init(_my_attack_entity);
            eAttackType = EAttackType.DISTANCE;
        }

        private void SameTmpAttack(GameObject _prefab, float _damages, float _cooldown, float _speed) // TMP
        {
            if (isAttacking || isInCooldown)
                return;

            GameObject bullet = Instantiate(_prefab);
            bullet.GetComponent<SpriteSceneObject>().location = myAttackEntity.transform.GetChild(0).GetComponent<SceneObject>().transform.position.ToGameSpace();
            bullet.tag = myAttackEntity.tag;

            TriggerBaseAttack trigger_attack = bullet.GetComponent<TriggerBaseAttack>();
            trigger_attack.damages = _damages;
            trigger_attack.onEntityHit += DamageEntity;
            if (isPlayer)
                trigger_attack.onEntityHit += ((PlayerEntity) myAttackEntity).OnEntityHit;

            Destroy(bullet, bulletLifeTime);

            myAttackEntity.StartCoroutine(BulletAddForceCoroutine(
                bullet.GetComponent<SpriteSceneObject>(),
                new Vector3(1 * Mathf.Sign(myAttackEntity.transform.localScale.x), 0, 0), 
                _speed));

            StartCooldown(_cooldown);
        }

        public override void LightGroundedAttack()
        {
            SameTmpAttack(weakBulletPrefab, weakDamages, weakCoolDown, weakBulletSpeed);
        }

        public override void HeavyGroundedAttack()
        {
            SameTmpAttack(heavyBulletPrefab, heavyDamages, heavyCoolDown, heavyBulletSpeed);
        }

        public override void StartCooldown(float _cooldown)
        {
            myAttackEntity.StartCoroutine(Cooldown(_cooldown));
        }

        private IEnumerator BulletAddForceCoroutine(SceneObject _bullet, Vector3 _direction, float _speed)
        {
            while (true)
            {
                _bullet.location += _direction * Time.deltaTime * _speed;
                yield return null;
            }
        }

        private IEnumerator Cooldown(float _cooldown)
        {
            isInCooldown = true;
            float time = _cooldown;

            while (time >= 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            isInCooldown = false;
        }
    }
}
