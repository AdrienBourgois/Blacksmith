using Game.Scripts.AttackBehavior;
using Game.Scripts.Entity;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Melee", menuName = "Attacks/Melee")]
    public class SoMeleeAttack : SoBaseAttack
    {
        public override void Attack(BaseEntity _entity)
        {
            base.Attack(_entity);

            MeleeBehavior melee = _entity.transform.GetChild(0).GetComponent<MeleeBehavior>();
            melee.damages = damages;
            melee.comboEffect = comboEffect;
            melee.horizontalVelocity = horizontalVelocity;
            melee.verticalVelocity = verticalVelocity;

            melee.gameObject.SetActive(true);
            melee.Attack();

            TimerManager.Instance.AddTimer("Attack feedback", 1f, true, false, () => { melee.gameObject.SetActive(false); });
        }
    }
}