using Game.Scripts.AttackBehavior;
using Game.Scripts.Entity;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Melee", menuName = "Attacks/Melee")]
    public class SoMeleeAttack : SoBaseAttack
    {
        [SerializeField] protected int childId;

        public override void Attack(BaseEntity _entity)
        {
            base.Attack(_entity);

            MeleeBehavior melee = _entity.transform.GetChild(childId).GetComponent<MeleeBehavior>();
            melee.hitData.damages = hitData.damages;
            melee.hitData.comboEffect = hitData.comboEffect;
            melee.hitData.xVelocity = hitData.xVelocity;
            melee.hitData.yVelocity = hitData.yVelocity;

            //melee.gameObject.SetActive(true);
            melee.Attack();

            TimerManager.Instance.AddTimer("Attack feedback", coolDown, true, false, () => { melee.gameObject.SetActive(false); });
        }
    }
}