using System.Collections;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu (fileName = "CaCAttack", menuName = "Attacks/CaCAttack")]
    public class SoCaCAttack : SoBaseAttack
    {
        private void SameTmpAttack() // TMP
        {
            if (isAttacking || isInCooldown)
                return;

            myAttackEntity.transform.GetChild(0).gameObject.SetActive(true);
            myAttackEntity.transform.GetChild(0).GetComponent<TriggerCaCAttack>().Attack();
            myAttackEntity.StartCoroutine(AttackDuration(1f));
        }

        public override void Init(AttackEntity _my_attack_entity)
        {
            base.Init(_my_attack_entity);

            eAttackType = EAttackType.CAC;

            TriggerBaseAttack trigger_attack = myAttackEntity.transform.GetChild(0).GetComponent<TriggerBaseAttack>();
            trigger_attack.damages = damages;
            trigger_attack.onEntityHit += DamageEntity;
            if (isPlayer)
                trigger_attack.onEntityHit += ((PlayerEntity)myAttackEntity).OnEntityHit;
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

        private IEnumerator Cooldown()
        {
            isInCooldown = true;
            float time = coolDown;

            while (time >= 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            isInCooldown = false;
        }

        private IEnumerator AttackDuration(float _duration)
        {
            isAttacking = true;
            float time = _duration;

            while (time >= 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            isAttacking = false;

            myAttackEntity.transform.GetChild(0).gameObject.SetActive(false);
            StartCooldown();
        }
    }
}
