using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.AI
{
    public class SelectTargetState : IEnemyState
    {
        private EnemyBehavior myBehavior;

        public SelectTargetState(EnemyBehavior _behavior) { myBehavior = _behavior; }

        public void ToIdleState()
        {
        }

        public void ToSelectTargetState()
        {
            // this
        }

        public void ToChaseState()
        {
            BaseEntity target = EntityManager.Instance.CurrentPlayer;
            myBehavior.ToChaseState(target);
        }

        public void ToAttackState()
        {
        }

        public void Update()
        {
            ToChaseState();
        }
    }
}
