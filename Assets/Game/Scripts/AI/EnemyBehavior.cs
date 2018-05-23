using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.AI
{
    public class EnemyBehavior
    {
        #region States
        private IdleState idleState;
        private SelectTargetState selectTargetState;
        private ChaseState chaseState;
        private AttackState attackState;

        private IEnemyState currentState;
        #endregion

        public EnemyEntity MyEntity { get; private set; }
        public BaseEntity Target { get; private set; }

        public float AttackDistance { get; private set; }

        public EnemyBehavior(EnemyEntity _entity, float _attack_distance)
        {
            MyEntity = _entity;

            idleState = new IdleState(this);
            selectTargetState = new SelectTargetState(this);
            chaseState = new ChaseState(this);
            attackState = new AttackState(this);

            currentState = idleState;

            AttackDistance = _attack_distance;
        }

        public void ToChaseState(BaseEntity _target)
        {
            Target = _target;
            currentState = chaseState;
        }

        public void ToAttackState()
        {
            currentState = attackState;
        }

        public void Update()
        {
            currentState.Update();
        }

        public void ToSelectTargetState()
        {
            currentState = selectTargetState;
        }

        public bool IsInRange()
        {
            float dist = Vector3.Distance(MyEntity.location, Target.location);
            return dist < AttackDistance;
        }
    }
}