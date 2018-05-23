using System;

namespace Game.Scripts.AI
{
    public class AttackState : IEnemyState
    {
        private EnemyBehavior myBehavior;
        private Random rand;

        public AttackState(EnemyBehavior _behavior) { myBehavior = _behavior; }

        public void ToIdleState()
        {

        }

        public void ToSelectTargetState()
        {

        }

        public void ToChaseState()
        {
            if (!myBehavior.IsInRange())
                ToChaseState();
        }

        public void ToAttackState()
        {
            // this
        }

        public void Update()
        {
            if (myBehavior.MyEntity.CanAttack())
            {
                int i = UnityEngine.Random.Range(0, 2);
                if (i == 0)
                    myBehavior.MyEntity.LightGroundedAttack();
                else
                    myBehavior.MyEntity.HeavyGroundedAttack();
            }

            ToChaseState();
        }
    }
}