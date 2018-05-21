using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.AI
{
    public class AttackState : IEnemyState
    {
        private EnemyBehavior myBehavior;

        public AttackState(EnemyBehavior _behavior) { myBehavior = _behavior; }

        public void ToIdleState()
        {

        }

        public void ToSelectTargetState()
        {

        }

        public void ToChaseState()
        {

        }

        public void ToAttackState()
        {
            // this
        }

        public void Update()
        {

        }
    }
}