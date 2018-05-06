using System.Collections;
using Game.Scripts.AttackBehavior;
using Game.Scripts.Entity;
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
            melee.damages = damages;

            melee.gameObject.SetActive(true);
            melee.Attack();

            //melee.gameObject.SetActive(false);
        }
    }
}