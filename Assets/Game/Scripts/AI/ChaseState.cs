using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.AI
{
    public class ChaseState : IEnemyState
    {
        private EnemyBehavior myBehavior;

        public ChaseState(EnemyBehavior _behavior) { myBehavior = _behavior; }

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
        }

        public void Update()
        {

        }
    }
