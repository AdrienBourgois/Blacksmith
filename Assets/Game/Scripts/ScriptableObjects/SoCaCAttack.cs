using System.Collections;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu (fileName = "CaCAttack", menuName = "Attacks/CaCAttack")]
    public class SoCaCAttack : SoBaseAttack
    {
        /*private void SameTmpAttack() // TMP
        {
            if (isAttacking || isInCooldown)
                return;

            myAttackEntity.transform.GetChild(0).gameObject.SetActive(true);
            myAttackEntity.transform.GetChild(0).GetComponent<TriggerCaCAttack>().Attack();
            myAttackEntity.StartCoroutine(AttackDuration(1f));
        }*/

        public override void Init(AttackEntity _my_attack_entity)
        {
            base.Init(_my_attack_entity);

            eAttackType = EAttackType.CAC;

            TriggerBaseAttack trigger_attack_weak = myAttackEntity.transform.GetChild(0).GetComponent<TriggerBaseAttack>();
            trigger_attack_weak.damages = weakDamages;
            trigger_attack_weak.onEntityHit += DamageEntity;

            TriggerBaseAttack trigger_attack_heavy = myAttackEntity.transform.GetChild(1).GetComponent<TriggerBaseAttack>();
            trigger_attack_heavy.damages = heavyDamages;
            trigger_attack_heavy.onEntityHit += DamageEntity;

            if (isPlayer)
            {
                trigger_attack_heavy.onEntityHit += ((PlayerEntity)myAttackEntity).OnEntityHit;
                trigger_attack_heavy.onEntityHit += ((PlayerEntity)myAttackEntity).OnEntityHit;
            }
        }

        public override void LightGroundedAttack()
        {
            if (isAttacking || isInCooldown)
                return;

            myAttackEntity.transform.GetChild(0).gameObject.SetActive(true);
            myAttackEntity.transform.GetChild(0).GetComponent<TriggerCaCAttack>().Attack();
            myAttackEntity.StartCoroutine(AttackDuration(1f, weakCoolDown));
        }

        public override void HeavyGroundedAttack()
        {
            if (isAttacking || isInCooldown)
                return;

            myAttackEntity.transform.GetChild(1).gameObject.SetActive(true);
            myAttackEntity.transform.GetChild(1).GetComponent<TriggerCaCAttack>().Attack();
            myAttackEntity.StartCoroutine(AttackDuration(0.5f, heavyCoolDown));
        }

        public override void StartCooldown(float _coolddown)
        {
            myAttackEntity.StartCoroutine(Cooldown(_coolddown));
        }

        private IEnumerator Cooldown(float _coolDown)
        {
            isInCooldown = true;
            float time = _coolDown;

            while (time >= 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            isInCooldown = false;
        }

        private IEnumerator AttackDuration(float _duration, float _cooldown)
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
            myAttackEntity.transform.GetChild(1).gameObject.SetActive(false);

            StartCooldown(_cooldown);
        }
    }
}
