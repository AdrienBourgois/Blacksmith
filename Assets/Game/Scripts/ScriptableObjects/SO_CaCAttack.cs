using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu (fileName = "CaCAttack", menuName = "Attacks/CaCAttack")]
    public class SO_CaCAttack : SO_BaseAttack
    {
        private void SameTmpAttack() // TMP
        {
            if (isAttacking || isInCooldwnl)
                return;

            Debug.Log("ATTACK !");

            myAttackEntity.transform.GetChild(0).gameObject.SetActive(true);
            myAttackEntity.StartCoroutine(AttackDuration(1f));
        }

        public override void Init(AttackEntity _my_attack_entity)
        {
            base.Init(_my_attack_entity);
            eAttackType = EAttackType.CAC;
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

        IEnumerator AttackDuration(float _duration)
        {
            Debug.Log("AttackDuration");

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
