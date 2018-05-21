using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.AI
{
    public class IdleState : IEnemyState
    {
        private EnemyBehavior myBehavior;

        public IdleState(EnemyBehavior _behavior) { myBehavior = _behavior; }

        public void ToIdleState()
        {
            // this
        }

        public void ToSelectTargetState()
        {
            if (true)
                myBehavior.currentState = myBehavior.selectTargetState;
        }

        public void ToChaseState()
        {
        }

        public void ToAttackState()
        {
        }

        public void Update()
        {
            ToSelectTargetState();
        }
    }
}
